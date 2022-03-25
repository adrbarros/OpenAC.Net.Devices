﻿// ***********************************************************************
// Assembly         : OpenAC.Net.Devices
// Author           : RFTD
// Created          : 20-12-2018
//
// Last Modified By : RFTD
// Last Modified On : 20-12-2018
// ***********************************************************************
// <copyright file="OpenFileStream.cs" company="OpenAC .Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2016 Projeto OpenAC .Net
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using OpenAC.Net.Core;
using OpenAC.Net.Core.Extensions;

namespace OpenAC.Net.Devices
{
    internal sealed class OpenFileStream : OpenDeviceStream
    {
        #region Fields

        private FileStream client;

        #endregion Fields

        #region Constructor

        public OpenFileStream(FileConfig config) : base(config)
        {
            Guard.Against<ArgumentException>(config.File.IsEmpty(), "Arquivo não informado.");
        }

        #endregion Constructor

        #region Properties

        protected override int Available => 0;

        #endregion Properties

        #region Methods

        protected override bool OpenInternal()
        {
            if (client != null) return false;
            if (Config is not FileConfig config) return false;

            client = File.Open(config.File, config.CreateIfNotExits ? FileMode.OpenOrCreate : FileMode.Open);
            Writer = new BinaryWriter(client);

            return true;
        }

        protected override bool CloseInternal()
        {
            if (client == null) return false;

            client?.Dispose();
            Writer?.Dispose();

            Writer = null;
            client = null;

            return true;
        }

        #endregion Methods

        #region Dispose Methods

        protected override void OnDisposing() => client?.Dispose();

        #endregion Dispose Methods
    }
}