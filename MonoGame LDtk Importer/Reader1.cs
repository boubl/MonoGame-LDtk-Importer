//using System;
//using System.Collections.Generic;
//using Microsoft.Xna.Framework.Content;


//namespace OgmoMapImporter
//{
//    public class Reader1 : ContentTypeReader<OgmoMapFile>
//    {
//        protected override OgmoMapFile Read(ContentReader input, OgmoMapFile existingInstance)
//        {
//            OgmoMapFile omf = new OgmoMapFile();
//            omf.ogmoVersion = input.ReadString();
//            omf.width = input.ReadInt32();
//            omf.height = input.ReadInt32();
//            omf.offsetX = input.ReadInt32();
//            omf.offsetY = input.ReadInt32();
//            omf.layers = new List<OgmoMapLayer>();

//            int layersCount = input.ReadInt32();
//            for (int i = 0; i < layersCount; i++)
//            {
//                OgmoMapLayer layer = new OgmoMapLayer();
//                layer.name = input.ReadString();
//                layer._eid = input.ReadString();
//                layer.offsetX = input.ReadInt32();
//                layer.offsetY = input.ReadInt32();
//                layer.gridCellWidth = input.ReadInt32();
//                layer.gridCellHeight = input.ReadInt32();
//                layer.gridCellsX = input.ReadInt32();
//                layer.gridCellsY = input.ReadInt32();

//                layer.type = input.ReadInt32();

//                if (layer.type == 0)
//                {
//                    layer.tileset = input.ReadString();
//                    int tileCount = input.ReadInt32();
//                    layer.data = new int[tileCount];
//                    for (int j = 0; j < tileCount; j++)
//                    {
//                        layer.data[j] = input.ReadInt32();
//                    }
//                    layer.exportMode = input.ReadInt32();
//                    layer.arrayMode = input.ReadInt32();
//                }
                
//                if (layer.type == 1)
//                {
//                    int gridCount = input.ReadInt32();
//                    layer.grid = new string[gridCount];
//                    for (int j = 0; j < gridCount; j++)
//                    {
//                        layer.grid[j] = input.ReadString();
//                    }
//                    layer.arrayMode = input.ReadInt32();
//                }
                
//                if (layer.type == 2)
//                {
//                    layer.entities = new List<OgmoMapEntitie>();
//                    int entitiesCount = input.ReadInt32();
//                    for (int j = 0; j < entitiesCount; j++)
//                    {
//                        OgmoMapEntitie entitie = new OgmoMapEntitie();
//                        entitie.name = input.ReadString();
//                        entitie.id = input.ReadInt32();
//                        entitie._eid = input.ReadString();
//                        entitie.x = input.ReadInt32();
//                        entitie.y = input.ReadInt32();
//                        entitie.width = input.ReadInt32();
//                        entitie.height = input.ReadInt32();
//                        entitie.originX = input.ReadInt32();
//                        entitie.originY = input.ReadInt32();
//                        entitie.rotation = input.ReadInt32();
//                        entitie.flippedX = input.ReadBoolean();
//                        entitie.flippedY = input.ReadBoolean();
//                        entitie.values = new Dictionary<string, object>();
//                        string doesItHaveCustomValues = input.ReadString();
//                        if (doesItHaveCustomValues == "yes")
//                        {
//                            int valuesCount = input.ReadInt32();
//                            int truc = 0;
//                            for (int k = 0; k < valuesCount; k++)
//                            {
//                                string type = input.ReadString();
//                                string key = input.ReadString();
//                                if (type == "string")
//                                {
//                                    entitie.values.Add(key, input.ReadString());
//                                }
//                                if (type == "int")
//                                {
//                                    entitie.values.Add(key, input.ReadInt32());
//                                }
//                                if (type == "bool")
//                                {
//                                    entitie.values.Add(key, input.ReadBoolean());
//                                }
//                                if (type == "float")
//                                {
//                                    entitie.values.Add(key, (float)input.ReadDouble());
//                                }
//                                truc++;
//                            }
//                            layer.entities.Add(entitie);
//                        }
//                    }
//                }
                
//                omf.layers.Add(layer);
//            }
//            return omf;
//        }
//    }
//}
