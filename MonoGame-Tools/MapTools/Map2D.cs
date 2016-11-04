using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Tools.MapTools
{
    /// <summary>
    /// Map class for storing a map layout.
    /// </summary>
    public class Map2D
    {
        private Texture2D m_texture;
        private int mapSize_width, mapSize_height;
        private int m_spriteWidth, m_spriteHeight;

        public Map2D(Texture2D p_texture, int p_mapSize_width, int p_mapSize_height, int sWidth, int sHeight)
        {
            m_texture = p_texture;
            mapSize_width = p_mapSize_width;
            mapSize_height = p_mapSize_height;
            m_spriteWidth = sWidth;
            m_spriteHeight = sHeight;
        }

        /// <summary>
        /// Draw the entire map
        /// </summary>
        /// <param name="p_sb"></param>
        public void Draw(SpriteBatch p_sb)
        {
            int n_x = mapSize_width / m_spriteWidth;
            int n_y = mapSize_height / m_spriteHeight;

            int spriteHeightCarryOver = 0;
            for (int i = 0; i < n_y; ++i)
            {
                int spriteWidthCarryOver = 0;
                for (int j = 0; j < n_x; ++j)
                {
                    p_sb.Draw(
                        m_texture, new Rectangle(spriteWidthCarryOver, spriteHeightCarryOver, m_spriteWidth, m_spriteHeight), Color.White
                        );
                    spriteWidthCarryOver += m_spriteWidth;
                }
                spriteHeightCarryOver += m_spriteHeight;
            }
        }

        /*
        private string m_name;
        private uint m_tileWidth, m_tile_height;
        private IDictionary<Vector3, Tile2D> m_tiles;

        public Map2D(ContentManager p_cm, string p_mapFile)
        { loadMap(p_cm, p_mapFile); }

        public Map2D()
        { m_tiles = new Dictionary<Vector3, Tile2D>(); }

        /// <summary>
        /// Name of map.
        /// </summary>
        public string Name
        { get { return m_name; } }

        /// <summary>
        /// Width of all tiles
        /// </summary>
        public uint TileWidth
        { get { return m_tileWidth; } }

        /// <summary>
        /// Height of all tiles
        /// </summary>
        public uint TileHeight
        { get { return m_tile_height; } }

        /// <summary>
        /// Gets a chunk of the map to display in a window.
        /// </summary>
        /// <param name="p_domain"></param>
        /// <returns></returns>
        public IEnumerable<GameObject> getDomain(object p_domain)
        {
            List<GameObject> domain = new List<GameObject>();
            throw new NotImplementedException();
            return domain;
        }

        /// <summary>
        /// Load a map from file.
        /// </summary>
        /// <param name="p_mapFile">Name and/or directory of map.</param>
        /// <returns>Did map successfully load?</returns>
        public bool loadMap(ContentManager p_cm, string p_mapFile)
        {
            throw new NotImplementedException();
            bool success = true;

            const string xmlExtension = ".xml";
            XmlDocument xmlDoc = null;
            try
            {
                m_tiles = new Dictionary<Vector3, Tile2D>();
                xmlDoc = new XmlDocument();
                string path = string.Format(
                    "{0}/{1}{2}", p_cm.RootDirectory, p_mapFile.Trim(), xmlExtension
                    );
                xmlDoc.Load(path);
                XmlLoad(xmlDoc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Extract information from 
        /// </summary>
        /// <param name="p_doc"></param>
        private void XmlLoad(XmlDocument p_doc)
        {

        }

        /// <summary>
        /// Draw a chunk of the map.
        /// </summary>
        public void drawDomain(SpriteBatch p_sb, object p_domain)
        {
            foreach (Tile2D tile in getDomain(p_domain))
                tile.Draw(p_sb);
        }
        */
    }
}
