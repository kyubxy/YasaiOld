// produces a texture

#version 330
out vec4 outputColor;
in vec2 texCoord;

uniform vec3 colour;
uniform float alpha;
uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord) * vec4(colour, alpha);
}
