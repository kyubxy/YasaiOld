// produces a texture

#version 330
in vec2 texCoord;

out vec4 outputColor;

uniform vec3 colour;
uniform float alpha;
uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord) * vec4(colour, alpha);
}
