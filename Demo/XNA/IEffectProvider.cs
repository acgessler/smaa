using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SMAADemo
{
    public interface IEffectProvider
    {
        Effect Get(string name);
        void Prefetch(string name);
    }
}

