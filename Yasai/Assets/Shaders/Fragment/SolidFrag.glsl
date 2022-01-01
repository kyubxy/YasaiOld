// produces a single colour

#version 330 core
out vec4 FragColor;

uniform vec3 colour;
uniform float alpha;

void main()
{
    FragColor = vec4(colour, alpha);
}