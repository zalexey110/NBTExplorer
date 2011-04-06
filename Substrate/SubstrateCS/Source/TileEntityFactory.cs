﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Map
{
    using NBT;
    using TileEntities;

    public class TileEntityFactory
    {
        private static Dictionary<string, Type> _registry;

        public static TileEntity Create (string type)
        {
            Type t;
            if (!_registry.TryGetValue(type, out t)) {
                return null;
            }

            return Activator.CreateInstance(t, new object[] { type }) as TileEntity;
        }

        public static TileEntity Create (NBT_Compound tree)
        {
            string type = tree["id"].ToNBTString();

            Type t;
            if (!_registry.TryGetValue(type, out t)) {
                return null;
            }

            TileEntity te = Activator.CreateInstance(t) as TileEntity;

            return te.LoadTreeSafe(tree);
        }

        public static Type Lookup (string type)
        {
            Type t;
            if (!_registry.TryGetValue(type, out t)) {
                return null;
            }

            return t;
        }

        public static void Register (string id, Type subtype)
        {
            _registry[id] = subtype;
        }

        static TileEntityFactory ()
        {
            _registry = new Dictionary<string, Type>();

            _registry["Chest"] = typeof(TileEntityChest);
            _registry["Furnace"] = typeof(TileEntityFurnace);
            _registry["MobSpawner"] = typeof(TileEntityMobSpawner);
            _registry["Music"] = typeof(TileEntityMusic);
            _registry["Sign"] = typeof(TileEntitySign);
            _registry["Trap"] = typeof(TileEntityTrap);
        }
    }
}
