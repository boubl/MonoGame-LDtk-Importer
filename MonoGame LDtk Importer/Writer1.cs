//using Microsoft.Xna.Framework.Content.Pipeline;
//using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Text.Json;

//namespace OgmoMapImporter
//{
//    [ContentTypeWriter]
//    class Writer1 : ContentTypeWriter<OgmoMapFile>
//    {
//        protected override void Write(ContentWriter output, OgmoMapFile value)
//        {
//            output.Write(value.ogmoVersion);
//            output.Write(value.width);
//            output.Write(value.height);
//            output.Write(value.offsetX);
//            output.Write(value.offsetY);
//            output.Write(value.layers.Count);

//            foreach (OgmoMapLayer layer in value.layers)
//            {
//                int type;
//                output.Write(layer.name);
//                output.Write(layer._eid);
//                output.Write(layer.offsetX);
//                output.Write(layer.offsetY);
//                output.Write(layer.gridCellWidth);
//                output.Write(layer.gridCellHeight);
//                output.Write(layer.gridCellsX);
//                output.Write(layer.gridCellsY);

//                if (layer.tileset != null)
//                {
//                    output.Write(0);
//                    type = 0;
//                }
//                else if (layer.grid != null)
//                {
//                    output.Write(1);
//                    type = 1;
//                }
//                else if (layer.entities != null)
//                {
//                    output.Write(2);
//                    type = 2;
//                }
//                else
//                {
//                    type = -1;
//                }

//                //Tile layer
//                if (type == 0)
//                {
//                    output.Write(layer.tileset);
//                    output.Write(layer.data.Length);
//                    for (int i = 0; i < layer.data.Length; i++)
//                    {
//                        output.Write(layer.data[i]);
//                    }
//                    output.Write(layer.exportMode);
//                    output.Write(layer.arrayMode);
//                }

//                //Grid layer
//                if (type == 1)
//                {
//                    output.Write(layer.grid.Length);
//                    for (int i = 0; i < layer.grid.Length; i++)
//                    {
//                        output.Write(layer.grid[i]);
//                    }
//                    output.Write(layer.arrayMode);
//                }

//                //Entitie layer
//                if (type == 2)
//                {
//                    output.Write(layer.entities.Count);
//                    foreach (OgmoMapEntitie entitie in layer.entities)
//                    {
//                        output.Write(entitie.name);
//                        output.Write(entitie.id);
//                        output.Write(entitie._eid);
//                        output.Write(entitie.x);
//                        output.Write(entitie.y);
//                        output.Write(entitie.width);
//                        output.Write(entitie.height);
//                        output.Write(entitie.originX);
//                        output.Write(entitie.originY);
//                        output.Write(entitie.rotation);
//                        output.Write(entitie.flippedX);
//                        output.Write(entitie.flippedY);
//                        if (entitie.values != null)
//                        {
//                            output.Write("yes");
//                            output.Write(entitie.values.Count);
//                            foreach (string val in entitie.values.Keys)
//                            {
//                                JsonElement js = (JsonElement)entitie.values[val];
//                                if (JsonValueKind.String == js.ValueKind)
//                                {
//                                    output.Write("string");
//                                    output.Write(val);
//                                    output.Write(js.ToString());
//                                }
//                                if (JsonValueKind.Number == js.ValueKind)
//                                {
//                                    double dbl;
//                                    int nt;
//                                    if (js.TryGetInt32(out nt))
//                                    {
//                                        output.Write("int");
//                                        output.Write(val);
//                                        output.Write(nt);
//                                    }
//                                    else if (js.TryGetDouble(out dbl))
//                                    {
//                                        output.Write("float");
//                                        output.Write(val);
//                                        output.Write(dbl);
//                                    }
//                                }
//                                if (JsonValueKind.False == js.ValueKind)
//                                {
//                                    output.Write("bool");
//                                    output.Write(val);
//                                    output.Write(false);
//                                }
//                                if (JsonValueKind.True == js.ValueKind)
//                                {
//                                    output.Write("bool");
//                                    output.Write(val);
//                                    output.Write(true);
//                                }
//                            }
//                        }
//                        else
//                        {
//                            output.Write("no");
//                        }
//                    }
//                }
//            }
//        }

//        public override string GetRuntimeType(TargetPlatform targetPlatform)
//        {
//            return typeof(OgmoMapFile).AssemblyQualifiedName;
//        }

//        public override string GetRuntimeReader(TargetPlatform targetPlatform)
//        {
//            return typeof(Reader1).AssemblyQualifiedName;
//        }
//    }
//}
