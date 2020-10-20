﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Motel.Core
{
    public partial class SecureRandomNumberGenerator : RandomNumberGenerator
    {
         #region Field

        private bool _disposed = false;
        private readonly RandomNumberGenerator _rng;

        #endregion
        #region Ctor
        public SecureRandomNumberGenerator()
        {
            _rng = new RNGCryptoServiceProvider();
        }
        #endregion

        #region Method

        public int Next()
        {
            var data = new byte[sizeof(int)];
            _rng.GetBytes(data);
            return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
        }
        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            return (int)Math.Floor(minValue + ((double)maxValue - minValue) * NextDouble());
        }
        public double NextDouble()
        {
            var data = new byte[sizeof(uint)];
            _rng.GetBytes(data);
            var randUint = BitConverter.ToUInt32(data, 0);
            return randUint / (uint.MaxValue + 1.0);
        }

        public override void GetBytes(byte[] data)
        {
             _rng.GetBytes(data);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            _rng.GetNonZeroBytes(data);
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _rng?.Dispose();
            }

            _disposed = true;
        }
        #endregion
    }
}
