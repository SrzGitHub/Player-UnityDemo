Shader "Letin/LetinNormalVideoGl"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Lighting Off
		Cull Off

		Pass
		{
GLSLPROGRAM
#pragma only_renderers gles gles3
#version 300 es
#extension GL_OES_EGL_image_external_essl3 : require

#ifdef VERTEX
uniform vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform vec4 hlslcc_mtx4x4unity_MatrixVP[4];
in highp vec4 in_POSITION0;
in highp vec2 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
vec4 u_xlat0;
vec2 u_xlat1;
vec2 u_xlat4;

void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1.xy = vec2(u_xlat0.y * hlslcc_mtx4x4unity_MatrixVP[1].z, u_xlat0.y * hlslcc_mtx4x4unity_MatrixVP[1].w);
    u_xlat0.xy = hlslcc_mtx4x4unity_MatrixVP[0].zw * u_xlat0.xx + u_xlat1.xy;
    u_xlat0.xy = hlslcc_mtx4x4unity_MatrixVP[2].zw * u_xlat0.zz + u_xlat0.xy;
    u_xlat0.xy = hlslcc_mtx4x4unity_MatrixVP[3].zw * u_xlat0.ww + u_xlat0.xy;
    u_xlat4.xy = u_xlat0.yy * in_POSITION0.xy;
    vec4 tempPosition = vec4(u_xlat4.xy + u_xlat4.xy, u_xlat0.xy);
    gl_Position = vec4(in_POSITION0.xy * tempPosition.w * 2.0, tempPosition.zw);

    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * vec2(1.0, -1.0) + vec2(0.0, 1.0);
}
#endif  // VERTEX

#ifdef FRAGMENT
precision highp int;
uniform lowp samplerExternalOES _MainTex;
in highp vec2 vs_TEXCOORD0;
layout(location = 0) out mediump vec4 SV_Target0;
lowp vec4 u_xlat10_0;

void main()
{
    u_xlat10_0 = texture(_MainTex, vs_TEXCOORD0.xy);
    SV_Target0 = u_xlat10_0;
}
#endif // FRAGMENT

ENDGLSL
		}
	}

	Fallback "Letin/LetinNormalVideoCg"
}
