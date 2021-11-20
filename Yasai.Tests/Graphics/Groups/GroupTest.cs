using Xunit;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.Tests.Graphics.Groups
{
    public class GroupTest
    {
        class TestClient : Drawable
        {
            public int Dependency => Dependencies?.Resolve<int>() ?? -1;
        }
        
        [Fact]
        void testDependencyInjection()
        {
            TestClient client;
            
            Group group = new Group(new IDrawable[]
            {
                client = new TestClient()
            });
            
            DependencyContainer dependencyContainer = new DependencyContainer();
            dependencyContainer.Register<int>(69);
            group.Load(dependencyContainer);

            Assert.Equal(69, client.Dependency);
        }
        
        [Fact]
        void testLayeredDependencyInjection()
        {
             TestClient client;
             
             Group group = new Group(new IDrawable[]
             {
                 new Group(new IDrawable[]
                 {
                    new Group(new IDrawable[]
                    {
                        new Group(new IDrawable[]
                        {
                            new Group (new IDrawable[]
                            {
                                new Group(new IDrawable[]
                                {
                                    client = new TestClient()
                                })
                            })
                        })                  
                    })
                 })
             });
 
             DependencyContainer dependencyContainer = new DependencyContainer();
             dependencyContainer.Register<int>(69);
             group.Load(dependencyContainer);
 
             Assert.Equal(69, client.Dependency);           
        }
    }
}