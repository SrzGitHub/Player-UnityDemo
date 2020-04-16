Shader "Letin/LetinVrVideoGl"
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
#extension GL_OVR_multiview2 : require
#extension GL_OES_EGL_image_external_essl3 : require

#ifdef VERTEX
uniform vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform vec4 _MainTex_ST;
layout(std140) uniform UnityStereoGlobals
{
    vec4 hlslcc_mtx4x4unity_StereoMatrixP[8];
    vec4 hlslcc_mtx4x4unity_StereoMatrixV[8];
    vec4 hlslcc_mtx4x4unity_StereoMatrixInvV[8];
    vec4 hlslcc_mtx4x4unity_StereoMatrixVP[8];
    vec4 hlslcc_mtx4x4unity_StereoCameraProjection[8];
    vec4 hlslcc_mtx4x4unity_StereoCameraInvProjection[8];
    vec4 hlslcc_mtx4x4unity_StereoWorldToCamera[8];
    vec4 hlslcc_mtx4x4unity_StereoCameraToWorld[8];
    vec3 unity_StereoWorldSpaceCameraPos[2];
    vec4 unity_StereoScaleOffset[2];
};
layout(num_views = 2) in;
in highp vec4 in_POSITION0;
in highp vec2 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
vec4 u_xlat0;
int u_xlati1;
vec4 u_xlat2;

void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlati1 = int(gl_ViewID_OVR) << 2;
    u_xlat2 = u_xlat0.yyyy * hlslcc_mtx4x4unity_StereoMatrixVP[(u_xlati1 + 1)];
    u_xlat2 = hlslcc_mtx4x4unity_StereoMatrixVP[u_xlati1] * u_xlat0.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_StereoMatrixVP[(u_xlati1 + 2)] * u_xlat0.zzzz + u_xlat2;
    vec4 tempPosition = hlslcc_mtx4x4unity_StereoMatrixVP[(u_xlati1 + 3)] * u_xlat0.wwww + u_xlat2;
    gl_Position = vec4(in_POSITION0.xy * tempPosition.w * 2.0, tempPosition.zw);

    vec2 tempUv = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    vs_TEXCOORD0.xy = vec2((tempUv.x + float(gl_ViewID_OVR)) * 0.5, 1.0 - tempUv.y);
}
#endif  // VERTEX

#ifdef FRAGMENT
precision highp int;
uniform samplerExternalOES _MainTex;
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

    Fallback "Letin/LetinVrVideoCg"
}
