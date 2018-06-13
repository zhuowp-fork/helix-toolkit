﻿/*
The MIT License (MIT)
Copyright (c) 2018 Helix Toolkit contributors
*/
using System;
#if !NETFX_CORE
namespace HelixToolkit.Wpf.SharpDX.Model
#else
namespace HelixToolkit.UWP.Model
#endif
{
    using Render;
    using Shaders;
    public sealed class NormalMaterialCore : MaterialCore
    {
        public override IEffectMaterialVariables CreateMaterialVariables(IEffectsManager manager)
        {
            return new PassOnlyMaterialVariable(DefaultPassNames.Normals);
        }
    }

    public sealed class ColorMaterialCore : MaterialCore
    {
        public override IEffectMaterialVariables CreateMaterialVariables(IEffectsManager manager)
        {
            return new PassOnlyMaterialVariable(DefaultPassNames.Colors);
        }
    }

    public sealed class PositionMaterialCore : MaterialCore
    {
        public override IEffectMaterialVariables CreateMaterialVariables(IEffectsManager manager)
        {
            return new PassOnlyMaterialVariable(DefaultPassNames.Positions);
        }
    }

    public sealed class PassOnlyMaterialVariable : IEffectMaterialVariables
    {
        public ShaderPass MaterialPass { private set; get; }

        public bool RenderDiffuseMap { set; get; }
        public bool RenderDiffuseAlphaMap { set; get; }
        public bool RenderNormalMap { set; get; }
        public bool RenderDisplacementMap { set; get; }
        public bool RenderShadowMap { set; get; }
        public bool RenderEnvironmentMap { set; get; }
        public string DefaultShaderPassName { set; get; }

#pragma warning disable CS0067
        public event EventHandler<EventArgs> OnInvalidateRenderer;
#pragma warning restore CS0067

        private readonly string passName;
        public PassOnlyMaterialVariable(string passName)
        {
            this.passName = passName;
        }

        public bool Attach(IRenderTechnique technique)
        {
            MaterialPass = technique[passName];
            return !MaterialPass.IsNULL;
        }

        public bool BindMaterialTextures(DeviceContextProxy context, ShaderPass shaderPass)
        {
            return true;
        }

        public bool UpdateMaterialVariables(ref ModelStruct modelstruct)
        {
            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PassOnlyMaterialVariable() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}