using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Yasai.Graphics.Shaders
{
    public class Shader
    {
        private int handle;
        private readonly Dictionary<string,int> _uniformLocations;

        public Shader(string vertexPath, string fragPath)
        {
            // read shaders
            string VertexShaderSource;

            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
                VertexShaderSource = reader.ReadToEnd();

            string FragmentShaderSource;

            using (StreamReader reader = new StreamReader(fragPath, Encoding.UTF8))
                FragmentShaderSource = reader.ReadToEnd();
            
            // generate shaders
            var VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            var FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);
            
            // compile shaders
            GL.CompileShader(VertexShader);

            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            GL.CompileShader(FragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);

            if (infoLogFrag != System.String.Empty)
                System.Console.WriteLine(infoLogFrag);
            
            // more shit
            handle = GL.CreateProgram();

            GL.AttachShader(handle, VertexShader);
            GL.AttachShader(handle, FragmentShader);

            GL.LinkProgram(handle);
            
            // cleanup
            GL.DetachShader(handle, VertexShader);
            GL.DetachShader(handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
            
            GL.GetProgram(handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms); 
            _uniformLocations = new Dictionary<string, int>();

            // Loop over all the uniforms,
            for (var i = 0; i < numberOfUniforms; i++)
            {
                // get the name of this uniform,
                var key = GL.GetActiveUniform(handle, i, out _, out _);

                // get the location,
                var location = GL.GetUniformLocation(handle, key);

                // and then add it to the dictionary.
                _uniformLocations.Add(key, location);
            }
        }

        public int GetAttribLocation(string name) => GL.GetAttribLocation(handle, name);

        public void SetInt(string name, int value)
        {
            Use();
            GL.Uniform1(_uniformLocations[name], value);
        }
        
        public void SetMatrix4(string name, Matrix4 value)
        {
            Use();
            GL.UniformMatrix4(_uniformLocations[name], true, ref value);
        }
        
        public void SetVector3(string name, Vector3 data)
        {
            Use();
            GL.Uniform3(_uniformLocations[name], data);
        }
        
        public void Use() => GL.UseProgram(handle);
        
        private bool disposedValue;
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}