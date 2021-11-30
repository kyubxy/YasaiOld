using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Yasai.Resources;

namespace Yasai.Graphics.Shaders
{
    public class Shader : Resource<int>
    {
        //private int handle;
        private readonly Dictionary<string,int> uniformLocations;

        public Shader(string vertexPath, string fragPath) 
        {
            // read shaders
            string vertexShaderSource;

            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
                vertexShaderSource = reader.ReadToEnd();

            string fragmentShaderSource;

            using (StreamReader reader = new StreamReader(fragPath, Encoding.UTF8))
                fragmentShaderSource = reader.ReadToEnd();
            
            // generate shaders
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            
            // compile shaders
            GL.CompileShader(vertexShader);

            string infoLogVert = GL.GetShaderInfoLog(vertexShader);
            if (infoLogVert != String.Empty)
                Console.WriteLine(infoLogVert);

            GL.CompileShader(fragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);

            if (infoLogFrag != String.Empty)
                Console.WriteLine(infoLogFrag);
            
            // more shit
            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            GL.LinkProgram(Handle);
            
            // cleanup
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
            
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms); 
            uniformLocations = new Dictionary<string, int>();

            // Loop over all the uniforms,
            for (var i = 0; i < numberOfUniforms; i++)
            {
                // get the name of this uniform,
                var key = GL.GetActiveUniform(Handle, i, out _, out _);

                // get the location,
                var location = GL.GetUniformLocation(Handle, key);

                // and then add it to the dictionary.
                uniformLocations.Add(key, location);
            }
        }

        public int GetAttribLocation(string name) => GL.GetAttribLocation(Handle, name);

        public void SetInt(string name, int value)
        {
            Use();
            GL.Uniform1(uniformLocations[name], value);
        }
        
        public void SetMatrix4(string name, Matrix4 value)
        {
            Use();
            GL.UniformMatrix4(uniformLocations[name], true, ref value);
        }
        
        public void SetVector3(string name, Vector3 data)
        {
            Use();
            GL.Uniform3(uniformLocations[name], data);
        }
        
        public void Use() => GL.UseProgram(Handle);
        
        #region IDisposable pattern
        private bool disposedValue;
        
        private void dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }

        public override void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }
        # endregion
    }
}