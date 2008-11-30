
#region ================== Copyright (c) 2007 Pascal vd Heiden

/*
 * Copyright (c) 2007 Pascal vd Heiden, www.codeimp.com
 * This program is released under GNU General Public License
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 */

#endregion

#region ================== Namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;
using CodeImp.DoomBuilder.Map;
using SlimDX.Direct3D9;
using SlimDX;
using CodeImp.DoomBuilder.Geometry;
using System.Drawing.Imaging;
using CodeImp.DoomBuilder.Data;
using CodeImp.DoomBuilder.Editing;
using CodeImp.DoomBuilder.IO;
using CodeImp.DoomBuilder.Rendering;

#endregion

namespace CodeImp.DoomBuilder.VisualModes
{
	public abstract class VisualGeometry : IComparable<VisualGeometry>
	{
		#region ================== Variables

		// Texture
		private ImageData texture;
		
		// Vertices
		private WorldVertex[] vertices;
		private int triangles;
		
		// Sector
		private VisualSector sector;
		
		// Rendering
		private int renderpass;
		
		// Sector buffer info
		private int vertexoffset;
		
		#endregion

		#region ================== Properties
		
		// Internal properties
		internal WorldVertex[] Vertices { get { return vertices; } }
		internal int VertexOffset { get { return vertexoffset; } set { vertexoffset = value; } }
		internal int Triangles { get { return triangles; } }
		internal int RenderPassInt { get { return renderpass; } }

		/// <summary>
		/// Render pass in which this geometry must be rendered. Default is Solid.
		/// </summary>
		public RenderPass RenderPass { get { return (RenderPass)renderpass; } set { renderpass = (int)value; } }

		/// <summary>
		/// Image to use as texture on this geometry.
		/// </summary>
		public ImageData Texture { get { return texture; } set { texture = value; } }

		/// <summary>
		/// Returns the VisualSector this geometry has been added to.
		/// </summary>
		public VisualSector Sector { get { return sector; } internal set { sector = value; } }
		
		#endregion

		#region ================== Constructor / Destructor
		
		// Constructor
		public VisualGeometry()
		{
			// Defaults
			renderpass = (int)RenderPass.Solid;
		}

		#endregion

		#region ================== Methods
		
		// This sets the vertices for this geometry
		protected void SetVertices(ICollection<WorldVertex> verts)
		{
			// Copy vertices
			vertices = new WorldVertex[verts.Count];
			verts.CopyTo(vertices, 0);
			triangles = vertices.Length / 3;
			if(sector != null) sector.NeedsUpdateGeo = true;
		}
		
		// This compares for sorting
		public int CompareTo(VisualGeometry other)
		{
			// Compare sectors
			return this.sector.Sector.Index - other.sector.Sector.Index;
		}

		#endregion
	}
}