/**
 * This XNA4/C# port of SMAA is (c) 2012, Alexander Christoph Gessler
 * It is released as Open Source under the same conditions as SMAA itself.
 * 
 * Check out LICENSE.txt in the root folder of the repository or
 * Readme.txt in /Demo/XNA for more information.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SMAADemo
{
    /** Implementation of IEffectProvider via XNA content pipeline with optional logging */
    public class DefaultEffectProvider : IEffectProvider
    {
        private Dictionary<string, Effect> Cache;
        private ContentManager Content;
        private ILogger Logger;
        private string Prefix;

        /** Construct an effect provider given a ContentManager
         *
         * @param _Content valid XNA content manager
         * @param _Logger optional logging instance
         * @param _Prefix prefix to add to all effects before loading them
         *   using the supplied content manager. 
         */
        public DefaultEffectProvider(ContentManager _Content, ILogger _Logger = null, 
            string _Prefix = "shaders/")
        {
            Logger = _Logger;
            Content = _Content;
            Prefix = _Prefix;

            Debug.Assert(_Content != null);
            Debug.Assert(_Prefix != null);

            Cache = new Dictionary<string, Effect>();
        }


        /** Get an effect given a resource name.
         * @param name Unique resource name to be loaded, the prefix supplied
         *   to the constructor will be prepended before passing it to
         *   the content pipeline.
         * @throws If the resource could no be loaded */
        public Effect Get(string name)
        {
            if (!Cache.ContainsKey(name))
            {
                Create(name);
            }
            return Cache[name];
        }


        /** Equivalent to Get() in this implementation. */
        public void Prefetch(string name)
        {
            // right now there is no difference between just
            // pre-fetching effects and actually using them.
            Get(name);
        }


        private void Create(string name)
        {
            Cache[name] = Content.Load<Effect>(Prefix + name);

            if (Logger != null)
            {
                Logger.Log("Created effect: " + name);
            }
        }
    }
}
