using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using Yasai.Structures.DI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics.Shaders;
using Vector2 = OpenTK.Mathematics.Vector2;
using Vector3 = OpenTK.Mathematics.Vector3;

namespace Yasai.Graphics
{
    public class Drawable : IDrawable
    {
        public Drawable Parent { get; set; }

        /// <summary>
        /// Relative to parent
        /// </summary>
        public virtual Vector2 Position { get; set; } = Vector2.Zero;

        /// <summary>
        /// Relative to image where the range is from [-1,1]
        /// </summary>
        public virtual Vector2 Offset { get; set; } = Vector2.Zero;

        public virtual Anchor Anchor { get; set; } = Anchor.TopLeft;
        public virtual Anchor Origin { get; set; } = Anchor.TopLeft;
        public virtual float Rotation { get; set; } = 0;

        public virtual Vector2 Size { get; set; } = new(100);
        public virtual Axes RelativeAxes { get; set; } = Axes.None;

        public virtual bool Visible { get; set; } = true;
        public virtual bool Enabled { get; set; } = true;
        public virtual Shader Shader { get; set; }

        // TODO: use a bindable 
        private float alpha = 1;

        public virtual float Alpha
        {
            get => alpha * (Parent?.Alpha ?? 1);
            set => alpha = value;
        }

        public virtual Color Colour { get; set; } = Color.White;

        // TODO: also use a bindable 
        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Position.Y);
        }

        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(Position.X, value);
        }

        public float Width
        {
            get => Size.X;
            set => Size = new Vector2(value, Size.Y);
        }

        public float Height
        {
            get => Size.Y;
            set => Size = new Vector2(Size.X, value);
        }

        private ITransform parentTransform => Parent?.AbsoluteTransform ?? new Transform(
            position: Vector2.Zero,
            size: new Vector2(100),
            rotation: 0,
            anchor: Anchor.TopLeft,
            origin: Anchor.TopLeft,
            offset: Vector2.Zero
        );

        // everything works except rotation
        private Vector2 pos => Position + parentTransform.Position;
        private Vector2 anchor => parentTransform.Size * AnchorToUnit(Anchor);
        private Vector2 origin => Size * (AnchorToUnit(Origin) + Offset);

        public Transform AbsoluteTransform => new(
            position: pos - origin + anchor,
            size: Size,
            rotation: Rotation + parentTransform.Rotation,
            anchor: Anchor,
            origin: Origin,
            offset: Offset
        );

        Vector2 sizing
        {
            get
            {
                var originalSize = AbsoluteTransform.Size / 2;

                Vector2 ret;
                
                switch (RelativeAxes)
                {
                    case Axes.None:
                        ret = originalSize;
                        break;
                    case Axes.Both:
                        ret = parentTransform.Size * originalSize;
                        break;
                    case Axes.X:
                        ret = new Vector2(parentTransform.Size.X * originalSize.X, originalSize.Y);
                        break;
                    case Axes.Y:
                        ret = new Vector2(originalSize.X, parentTransform.Size.Y * originalSize.Y);
                        break;
                    default:
                        throw new SwitchExpressionException(); 
                }

                return ret;
            }
        }
        
        // booba piss

        // TODO: make this not suck
        // how to actually draw
        // i have no idea what im doing
        public Matrix4 ModelTransforms =>
            // origin
            Matrix4.CreateTranslation(-new Vector3(AnchorToUnit(Origin)) * 2 + new Vector3(1)) *
            
            // rotation
            Matrix4.CreateRotationZ(AbsoluteTransform.Rotation) *
            
            // undo origin
            Matrix4.Invert(Matrix4.CreateTranslation(-new Vector3(AnchorToUnit(Origin)) * 2 + new Vector3(1))) *
            
            // move back to top left
            Matrix4.CreateTranslation(1, 1, 0) *
            
            // scale
            Matrix4.CreateScale(new Vector3(sizing)) *
            
            // position
            Matrix4.CreateTranslation(new Vector3(AbsoluteTransform.Position)) *
            Matrix4.Identity;
        
        /*
So there are lots of different subjects you can choose, but we just need to know: which one scales the best, and then which of the subjects will 
bring you the highest ATAR, yeah. So let’s have a look at the next page.

So the course score versus ATAR. So the course score is purely based on the performance and then ATAR is your position against everyone in the entire 
Canberra. So the course score is about student’s performance against the standard, whereas ATAR is about student’s position against all other students. 
But just note here the good performance doesn’t guarantee a high position. So for example you doing Maths Application: It doesn’t mean if you get straight 
As in every single session, you get a very high ATAR. So you need to make sure you have good performance and good subject. This will guarantee you a high position.

So when you choosing your courses, it is super important for you to consider first of all okay, what you’re good at and what you’re interested in: You don’t 
want to choose a subject you don’t enjoy it at all, it just, it will be a nightmare throughout 2 years. And, if you, if all of those subjects you interested in, 
the one is going to be your main majors is just going to be the one with the highest scaling factor, okay. And furthermore you can think about what you might want 
to do when you leave school, when you go to universities, that might be another thing you want to choose your courses. But the most important thing here should be your scaling factor.

Okay so for ATAR, ATAR is Australian Territory Admission Rank. It range from 30 to 99.95 with increment of 0.05, yeah. So after 99.95 the next one should be 99.90 etc etc. 
And you will never reach 100 and you will never, if someone tell you his ATAR is 99.99 he is absolutely lying. So your ATAR provides information about how well you have 
performed overall against all other students in entire Canberra, and also allow you to compare with other students who have completely different combination of course. 
It is a rank, not a mark. Okay so for example you get the ATAR of 70. So ATAR of 70 means you beat 70% of students, you are on the top 30%. So ATAR of 99 means you 
beat 99% of students in Canberra, and now you are on the top 1%. Okay so when calculating your ATARs: So to get an ATAR you need at least 3 majors and 1 minor okay, 
and your ATAR is based off 3.5 units of your majors, so each major you need to make sure you complete 3.5 units, and your minor you need 2 units. So, I just wrote it here, 
every 8 weeks of study is 0.5 units. So, for, if you wanna get a minor you need to complete a whole year of study, okay so around 32 weeks. However for a major, you just 
need to do 3 terms. Okay so 3 terms, 1 whole year plus 3 terms, it would be a major. And then just note here, you don’t need to count English towards your ATAR. But I 
think that’s just going to change very very soon, because in HSC and in VCE they need to count English towards their ATAR, so I think Canberra is going to follow the 
Australian Curriculum, so like later in the years English has to count towards the ATAR. But at this stage you don’t need to count English towards your ATAR.

So and then the next one is about subject selection and scaling. So different course have different groups of students. So, if you wanna compare different groups you need to 
make sure it’s fair, okay, so - what we do is we have something called the AST test, so AST test, so they calculate your marks and your position against all other students if 
they study the same thing. So once your marks are scaled, and we call that an aggregate mark, okay, an aggregate mark. And your aggregate mark is used to calculate your 3 
majors and 1 minors, and then once everyone in ACT have this aggregate mark, your position would be determined and then you will have your ATAR.

So if you want your ATAR, it’s all based on this AST test, we call it the ACT Scaling Test, so what do you need? You need your course score, which is your overall performance 
throughout the subject, and scaling factors for that specific subject, and then those two multiply together and then add up all the majors and minors, would be your aggregate scores. 
So let’s have a look at how to check your course scores first, so how do you check your overall performance throughout the subject. 

You go to the BSSS website, it’s really easy to find, so you just google BSSS, and this website will show up. This website will show up, and then you click here, information 
for students, and then the first tab profile online, and depends on if you from like Narrabundah College, Canberra College, Dickson College or Gungahlin College. So if you from 
ACT Government College you click the second one, however if you from Girls’ Grammar, Boys’ Grammar, Canberra Grammar School or Radford College, you just click this Non-Government 
Independent Students. And once you click that, and then once you click that, a tab will shows up, ask you the username and password. The username is 7 digits students ID, and the 
password is whatever the password you log on for your school email.

So once you put in the correct username and password, this page will show up. So it’ll be welcome [Your name] and your student ID. And then what we do is we click here, academic record. 
So you click the third tab, academic record. And then this page will show up. So what you do is you click this PDF copy and then you can download the student record, so your academic record. 
So the course score is just based on the all the scores you did throughout the sessions, throughout every semester of school. Just note the course score matters, this ABCD doesn’t really matter. 
So don’t worry about if you get ABCDE or whatever, just worry about this course score, those are the scores actually going to count towards your ATARs.

So this is your overall performance, so remember your ATAR depends on your performance and your subject, so which subject scales the best? I can’t tell you anything but the BSSS 
website will tell you. So you go to the BSSS website, instead of clicking “Information for Students”, you click here, “Year 12 Vocational Data”, and then this page will jump up. 
And then if you look at the last one, so you click here, Year 12 Study for 2012, and the last one, 4i, down the bottom, Scaled Scores Mean and Standard Deviation by Course Area. 
So I have already download them into my computer, so here, Scaled Score, so I have download all the scaled scores from 2012 to 2017. So we just look at some.

Every single year it is different, but the general trend is exactly the same. So we have a look at the 2016, and we also have a look at the 2017 one. So for the 2017 one, 
you can see every single school is showing here, Australian National Universities, and Birmingham School, and Canberra College, and Canberra Girls’ Grammar School, so every single school 
show up here. So let’s just go to Narrabundah College, okay so here is Narrabundah College. Narrabundah College, so for Narrabundah College we have different subject let’s just have a 
look at, yeah, which subject is better. So the one you looking at here, Scaled Scores Mean. So Scaled Scores mean just tells you, because in Canberra, doesn’t matter which school you are 
at, the average is usually all around 70, and the lowest score is usually around 40 and the highest score is usually above 100, or roughly at 100. But, a 70 in international schools or 70 
in private schools or a 70 in Narrabundah College, it’s all different, okay. So although a student’s getting a 70 average in all different schools, but their ability is completely different. 

So this scaled scores mean just tells you if you get a 70 in all these subjects, what scores will you get after the AST test? So this pretty much just tells you the score factor. 
So higher scaled score mean will just tells you this subject is going to scales better compared to other subject. So in Narrabundah College we have English, the scaled scores mean is 
151. And for Specialist Maths, the scaled scores mean is 177, Maths Method is 144.7, so you can see there is a big difference between Specialist Maths, Maths Method and Maths Application. 
That’s why you never see anyone top ACT by doing Maths Method, all the students top ACT or top their school is always the one doing double Specialist Maths, double Specialist Maths. 
So the best 4 subject you can look at the dux of every single year it’s usually the subject they do is double Specialist Maths, and Physics, super high scaled scores mean as well, 
and Chemistry. So those 4 subject, you can see it give you a dramatic advantage compared to all the other subject, okay, it’s so high. So even if you get average in those subject, 
the scaled score factor will bring your ATAR super super high.

However, we can look at the 2016 scores, okay. Because I said every single year is different so this is 2017, now let’s look at 2016. In 2016 again Narrabundah College, you can see 
Specialist Maths is now 178, so it.. yeah. So although it different but there is not big.. not big difference. So this is still 178. English last year, sorry, yea, in 2017, it’s 
151, now it’s 154, so you can see it’s roughly the same, and Physics and Chemistry again above 170, okay. Super high scaled scores. So this about the scaled scores. 

And then in the end when you graduate, you get Australian Capital Territory Year 12 Certificate, looks like this. Shows you all the subject you do, so as what I did, I did 
English, Specialist Maths, Physics, Chemistry as my 5 majors, and lots of other random minors. So those are the 5 subjects, and then they calculate the scaled scores, they pick the best 
4, however Physics scaled score is lower than my double Specialist Maths and Chemistry, so 60% of the minor is going to count towards my ATAR and my major which is double Specialist 
Maths and Chemistry, 100% count towards my ATAR. So you have your original scores multiplied by scaling factors, and then multiplied by weight, you get the weighted scaled scores. 
Add them up, this is the final aggregate score, okay, which is 800. 

So every single person in ACT will have a aggregate score, and then they just rank everyone in terms of their aggregate score. So the highest aggregate score will be 99.95, 
and the next one will be 99.90 etc etc. So you can see if you want to maximise your ATAR you need to maximise your aggregate score, which first of all you need to have good 
performance, also you need to make sure you choose the best subjects.

So overall just remember ATAR is a overall rank, not a mark, and you need to choose the course you like and you are good at, and you need to make sure you study hard. So thank you, 
if you like the video, please make sure you like, and leave a comment below if you have any questions. Thank you.
 
         */

        public virtual bool Loaded { get; protected set; }

        public virtual void Load(DependencyContainer container) 
        { }

        public virtual void Update(FrameEventArgs args)
        { }

        public virtual void Draw()
        { }
        
        public virtual void Dispose()
        {
            //Parent?.Dispose();
            //Shader?.Dispose();
        }
        
        public static Vector2 AnchorToUnit(Anchor anchor) => AnchorToUnit((int)anchor);
        public static Vector2 AnchorToUnit(int num) => new ((float)num % 3 / 2, (float)Math.Floor((double)num / 3) / 2);

        #region input
        
        public event Action<Vector2, MouseButtonEventArgs> MouseClickEvent;
        public event Action<Vector2, MouseButtonEventArgs> MousePressEvent;
        public event Action<MouseMoveEventArgs> MouseMoveEvent;
        public event Action MouseEnterEvent;
        public event Action MouseExitEvent;
        public event Action<Vector2, MouseWheelEventArgs> MouseScrollEvent;
        
        // mouse

        public bool EnableGlobalMousePress { get; set; }
        public bool EnableGlobalMouseMove { get; set; }
        public bool EnableMousePress { get; set; }
        public bool EnableMouseMove { get; set; }
        public bool EnableMouseScroll { get; set; }

        public virtual void GlobalMousePress(Vector2 position, MouseButtonEventArgs buttonArgs)
        {
        }

        public virtual void GlobalMouseMove(MouseMoveEventArgs args)
        {
        }

        public virtual bool MousePress(Vector2 position, MouseButtonEventArgs buttonArgs)
        {
            MousePressEvent?.Invoke(position, buttonArgs);
            return true;
        }

        public virtual bool MouseMove(MouseMoveEventArgs args)
        {
            MouseMoveEvent?.Invoke(args);
            return true;
        }

        public virtual bool MouseScroll(Vector2 position, MouseWheelEventArgs args)
        {
            MouseScrollEvent?.Invoke(position, args);
            return true;
        }

        // keyboard
        
        public event Action<KeyboardKeyEventArgs> KeyDownEvent;
        public event Action<KeyboardKeyEventArgs> KeyUpEvent;

        public bool EnableKeyDown { get; set; }
        public bool EnableKeyUp { get; set; }
        public virtual void KeyDown(KeyboardKeyEventArgs args) => KeyDownEvent?.Invoke(args);
        public virtual void KeyUp(KeyboardKeyEventArgs args) => KeyUpEvent?.Invoke(args);

        #endregion
    }
}