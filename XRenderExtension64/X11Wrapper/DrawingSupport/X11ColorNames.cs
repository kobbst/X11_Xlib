// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: April 2013
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2013 Steffen Ploetz
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// This copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// //////////////////////////////////////////////////////////////////////

using System;

namespace X11
{
	public struct ColorMapping
	{
		/// <summary>The name of the color.</summary>
		public string Name;
		
		/// <summary>The RGB Value of the color.</summary>
		public int    RGB;
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="name">The name of the color.<see cref="System.String"/></param>
		/// <param name="rgb">The RGB Value of the color.<see cref="System.Int32"/></param>
		public ColorMapping (string name, int rgb)
		{
			Name = name;
			RGB = rgb;
		}
	}
	
	public static class X11ColorNames
	{
		public static string	AliceBlue				= "AliceBlue";				//#F0F8FF
		public static string	AntiqueWhite			= "AntiqueWhite";			//#FAEBD7
		public static string	Aqua					= "Aqua";					//#00FFFF
		public static string	Aquamarine				= "Aquamarine";				//#7FFFD4
		public static string	Azure					= "Azure";					//#F0FFFF
		public static string	Beige					= "Beige";					//#F5F5DC
		public static string	Bisque					= "Bisque";					//#FFE4C4
		public static string	Black					= "Black";					//#000000
		public static string	BlanchedAlmond			= "BlanchedAlmond";			//#FFEBCD
		public static string	Blue					= "Blue";					//#0000FF
		public static string	BlueViolet				= "BlueViolet";				//#8A2BE2
		public static string	Brown					= "Brown";					//#A52A2A
		public static string	BurlyWood				= "BurlyWood";				//#DEB887
		public static string	CadetBlue				= "CadetBlue";				//#5F9EA0
		public static string	Chartreuse				= "Chartreuse";				//#7FFF00
		public static string	Chocolate				= "Chocolate";				//#D2691E
		public static string	Coral					= "Coral";					//#FF7F50
		public static string	CornflowerBlue			= "CornflowerBlue";			//#6495ED
		public static string	Cornsilk				= "Cornsilk";				//#FFF8DC
		public static string	Crimson					= "Crimson";				//#DC143C
		public static string	Cyan					= "Cyan";					//#00FFFF
		public static string	DarkBlue				= "DarkBlue";				//#00008B
		public static string	DarkCyan				= "DarkCyan";				//#008B8B
		public static string	DarkGoldenrod			= "DarkGoldenrod";			//#B8860B
		public static string	DarkGray				= "DarkGray";				//#A9A9A9
		public static string	DarkGreen				= "DarkGreen";				//#006400
		public static string	DarkKhaki				= "DarkKhaki";				//#BDB76B
		public static string	DarkMagenta				= "DarkMagenta";			//#8B008B
		public static string	DarkOliveGreen			= "DarkOliveGreen";			//#556B2F
		public static string	DarkOrange				= "DarkOrange";				//#FF8C00
		public static string	DarkOrchid				= "DarkOrchid";				//#9932CC
		public static string	DarkRed					= "DarkRed";				//#8B0000
		public static string	DarkSalmon				= "DarkSalmon";				//#E9967A
		public static string	DarkSeaGreen			= "DarkSeaGreen";			//#8FBC8F
		public static string	DarkSlateBlue			= "DarkSlateBlue";			//#483D8B
		public static string	DarkSlateGray			= "DarkSlateGray";			//#2F4F4F
		public static string	DarkTurquoise			= "DarkTurquoise";			//#00CED1
		public static string	DarkViolet				= "DarkViolet";				//#9400D3
		public static string	DeepPink				= "DeepPink";				//#FF1493
		public static string	DeepSkyBlue				= "DeepSkyBlue";			//#00BFFF
		public static string	DimGray					= "DimGray";				//#696969
		public static string	DodgerBlue				= "DodgerBlue";				//#1E90FF
		public static string	FireBrick				= "FireBrick";				//#B22222
		public static string	FloralWhite				= "FloralWhite";			//#FFFAF0
		public static string	ForestGreen				= "ForestGreen";			//#228B22
		public static string	Fuchsia					= "Fuchsia";				//#FF00FF
		public static string	Gainsboro				= "Gainsboro";				//#DCDCDC
		public static string	GhostWhite				= "GhostWhite";				//#F8F8FF
		public static string	Gold					= "Gold";					//#FFD700
		public static string	Goldenrod				= "Goldenrod";				//#DAA520
		public static string	Gray					= "Gray";					//#808080
		public static string	Green					= "Green";					//#008000
		public static string	GreenYellow				= "GreenYellow";			//#ADFF2F
		public static string	Honeydew				= "Honeydew";				//#F0FFF0
		public static string	HotPink					= "HotPink";				//#FF69B4
		public static string	IndianRed				= "IndianRed";				//#CD5C5C
		public static string	Indigo					= "Indigo";					//#4B0082
		public static string	Ivory					= "Ivory";					//#FFFFF0
		public static string	Khaki					= "Khaki";					//#F0E68C
		public static string	Lavender				= "Lavender";				//#E6E6FA
		public static string	LavenderBlush			= "LavenderBlush";			//#FFF0F5
		public static string	LawnGreen				= "LawnGreen";				//#7CFC00
		public static string	LemonChiffon			= "LemonChiffon";			//#FFFACD
		public static string	LightBlue				= "LightBlue";				//#ADD8E6
		public static string	LightCoral				= "LightCoral";				//#F08080
		public static string	LightCyan				= "LightCyan";				//#E0FFFF
		public static string	LightGoldenrodYellow	= "LightGoldenrodYellow";	//#FAFAD2
		public static string	LightGreen				= "LightGreen";				//#90EE90
		public static string	LightGrey				= "LightGrey";				//#D3D3D3
		public static string	LightPink				= "LightPink";				//#FFB6C1
		public static string	LightSalmon				= "LightSalmon";			//#FFA07A
		public static string	LightSeaGreen			= "LightSeaGreen";			//#20B2AA
		public static string	LightSkyBlue			= "LightSkyBlue";			//#87CEFA
		public static string	LightSlateGray			= "LightSlateGray";			//#778899
		public static string	LightSteelBlue			= "LightSteelBlue";			//#B0C4DE
		public static string	LightYellow				= "LightYellow";			//#FFFFE0
		public static string	Lime					= "Lime";					//#00FF00
		public static string	LimeGreen				= "LimeGreen";				//#32CD32
		public static string	Linen					= "Linen";					//#FAF0E6
		public static string	Magenta					= "Magenta";				//#FF00FF
		public static string	Maroon					= "Maroon";					//#800000
		public static string	MediumAquamarine		= "MediumAquamarine";		//#66CDAA
		public static string	MediumBlue				= "MediumBlue";				//#0000CD
		public static string	MediumOrchid			= "MediumOrchid";			//#BA55D3
		public static string	MediumPurple			= "MediumPurple";			//#9370DB
		public static string	MediumSeaGreen			= "MediumSeaGreen";			//#3CB371
		public static string	MediumSlateBlue			= "MediumSlateBlue";		//#7B68EE
		public static string	MediumSpringGreen		= "MediumSpringGreen";		//#00FA9A
		public static string	MediumTurquoise			= "MediumTurquoise";		//#48D1CC
		public static string	MediumVioletRed			= "MediumVioletRed";		//#C71585
		public static string	MidnightBlue			= "MidnightBlue";			//#191970
		public static string	MintCream				= "MintCream";				//#F5FFFA
		public static string	MistyRose				= "MistyRose";				//#FFE4E1
		public static string	Moccasin				= "Moccasin";				//#FFE4B5
		public static string	NavajoWhite				= "NavajoWhite";			//#FFDEAD
		public static string	Navy					= "Navy";					//#000080
		public static string	OldLace					= "OldLace";				//#FDF5E6
		public static string	Olive					= "Olive";					//#808000
		public static string	OliveDrab				= "OliveDrab";				//#6B8E23
		public static string	Orange					= "Orange";					//#FFA500
		public static string	OrangeRed				= "OrangeRed";				//#FF4500
		public static string	Orchid					= "Orchid";					//#DA70D6
		public static string	PaleGoldenrod			= "PaleGoldenrod";			//#EEE8AA
		public static string	PaleGreen				= "PaleGreen";				//#98FB98
		public static string	PaleTurquoise			= "PaleTurquoise";			//#AFEEEE
		public static string	PaleVioletRed			= "PaleVioletRed";			//#DB7093
		public static string	PapayaWhip				= "PapayaWhip";				//#FFEFD5
		public static string	PeachPuff				= "PeachPuff";				//#FFDAB9
		public static string	Peru					= "Peru";					//#CD853F
		public static string	Pink					= "Pink";					//#FFC0CB
		public static string	Plum					= "Plum";					//#DDA0DD
		public static string	PowderBlue				= "PowderBlue";				//#B0E0E6
		public static string	Purple					= "Purple";					//#800080
		public static string	Red						= "Red";					//#FF0000
		public static string	RosyBrown				= "RosyBrown";				//#BC8F8F
		public static string	RoyalBlue				= "RoyalBlue";				//#4169E1
		public static string	SaddleBrown				= "SaddleBrown";			//#8B4513
		public static string	Salmon					= "Salmon";					//#FA8072
		public static string	SandyBrown				= "SandyBrown";				//#F4A460
		public static string	SeaGreen				= "SeaGreen";				//#2E8B57
		public static string	Seashell				= "Seashell";				//#FFF5EE
		public static string	Sienna					= "Sienna";					//#A0522D
		public static string	Silver					= "Silver";					//#C0C0C0
		public static string	SkyBlue					= "SkyBlue";				//#87CEEB
		public static string	SlateBlue				= "SlateBlue";				//#6A5ACD
		public static string	SlateGray				= "SlateGray";				//#708090
		public static string	Snow					= "Snow";					//#FFFAFA
		public static string	SpringGreen				= "SpringGreen";			//#00FF7F
		public static string	SteelBlue				= "SteelBlue";				//#4682B4
		public static string	Tan						= "Tan";					//#D2B48C
		public static string	Teal					= "Teal";					//#008080
		public static string	Thistle					= "Thistle";				//#D8BFD8
		public static string	Tomato					= "Tomato";					//#FF6347
		public static string	Turquoise				= "Turquoise";				//#40E0D0
		public static string	Violet					= "Violet";					//#EE82EE
		public static string	Wheat					= "Wheat";					//#F5DEB3
		public static string	White					= "White";					//#FFFFFF
		public static string	WhiteSmoke				= "WhiteSmoke";				//#F5F5F5
		public static string	Yellow					= "Yellow";					//#FFFF00
		public static string	YellowGreen				= "YellowGreen";			//#9ACD32
		
		public static string	Gray0 					= "Gray0";					//#000000 
		public static string	Gray1 					= "Gray1";					//#030303 
		public static string	Gray2 					= "Gray2";					//#050505 
		public static string	Gray3 					= "Gray3";					//#080808 
		public static string	Gray4 					= "Gray4";					//#0a0a0a 
		public static string	Gray5 					= "Gray5";					//#0d0d0d 
		public static string	Gray6 					= "Gray6";					//#0f0f0f 
		public static string	Gray7 					= "Gray7";					//#121212 
		public static string	Gray8 					= "Gray8";					//#141414 
		public static string	Gray9 					= "Gray9";					//#171717 
		public static string	Gray10 					= "Gray10";					//#1A1A1A 
		public static string	Gray11 					= "Gray11";					//#1c1c1c 
		public static string	Gray12 					= "Gray12";					//#1f1f1f 
		public static string	Gray13 					= "Gray13";					//#212121 
		public static string	Gray14 					= "Gray14";					//#242424 
		public static string	Gray15 					= "Gray15";					//#262626 
		public static string	Gray16 					= "Gray16";					//#292929 
		public static string	Gray17 					= "Gray17";					//#2b2b2b 
		public static string	Gray18 					= "Gray18";					//#2e2e2e 
		public static string	Gray19 					= "Gray19";					//#303030 
		public static string	Gray20 					= "Gray20";					//#333333 
		public static string	Gray21 					= "Gray21";					//#363636 
		public static string	Gray22 					= "Gray22";					//#383838 
		public static string	Gray23 					= "Gray23";					//#3b3b3b 
		public static string	Gray24 					= "Gray24";					//#3d3d3d 
		public static string	Gray25 					= "Gray25";					//#404040 
		public static string	Gray26 					= "Gray26";					//#424242 
		public static string	Gray27 					= "Gray27";					//#454545 
		public static string	Gray28 					= "Gray28";					//#474747 
		public static string	Gray29 					= "Gray29";					//#4a4a4a 
		public static string	Gray30 					= "Gray30";					//#4d4d4d 
		public static string	Gray31 					= "Gray31";					//#4f4f4f 
		public static string	Gray32 					= "Gray32";					//#525252 
		public static string	Gray33 					= "Gray33";					//#545454 
		public static string	Gray34 					= "Gray34";					//#575757 
		public static string	Gray35 					= "Gray35";					//#595959 
		public static string	Gray36 					= "Gray36";					//#5c5c5c 
		public static string	Gray37 					= "Gray37";					//#5e5e5e 
		public static string	Gray38 					= "Gray38";					//#616161 
		public static string	Gray39 					= "Gray39";					//#636363 
		public static string	Gray40 					= "Gray40";					//#666666 
		public static string	Gray41 					= "Gray41";					//#696969 
		public static string	Gray42 					= "Gray42";					//#6b6b6b 
		public static string	Gray43 					= "Gray43";					//#6e6e6e 
		public static string	Gray44 					= "Gray44";					//#707070 
		public static string	Gray45 					= "Gray45";					//#737373 
		public static string	Gray46 					= "Gray46";					//#757575 
		public static string	Gray47 					= "Gray47";					//#787878 
		public static string	Gray48 					= "Gray48";					//#7a7a7a 
		public static string	Gray49 					= "Gray49";					//#7d7d7d 
		public static string	Gray50 					= "Gray50";					//#7f7f7f 
		public static string	Gray51 					= "Gray51";					//#828282 
		public static string	Gray52 					= "Gray52";					//#858585 
		public static string	Gray53 					= "Gray53";					//#878787 
		public static string	Gray54 					= "Gray54";					//#8a8a8a 
		public static string	Gray55 					= "Gray55";					//#8c8c8c 
		public static string	Gray56 					= "Gray56";					//#8f8f8f 
		public static string	Gray57 					= "Gray57";					//#919191 
		public static string	Gray58 					= "Gray58";					//#949494 
		public static string	Gray59 					= "Gray59";					//#969696 
		public static string	Gray60 					= "Gray60";					//#999999 
		public static string	Gray61 					= "Gray61";					//#9c9c9c 
		public static string	Gray62 					= "Gray62";					//#9e9e9e 
		public static string	Gray63 					= "Gray63";					//#A1A1A1 
		public static string	Gray64 					= "Gray64";					//#a3a3a3 
		public static string	Gray65 					= "Gray65";					//#a6a6a6 
		public static string	Gray66 					= "Gray66";					//#a8a8a8 
		public static string	Gray67 					= "Gray67";					//#ababab 
		public static string	Gray68 					= "Gray68";					//#adadad 
		public static string	Gray69 					= "Gray69";					//#b0b0b0 
		public static string	Gray70 					= "Gray70";					//#b3b3b3 
		public static string	Gray71 					= "Gray71";					//#b5b5b5 
		public static string	Gray72 					= "Gray72";					//#b8b8b8 
		public static string	Gray73 					= "Gray73";					//#bababa 
		public static string	Gray74 					= "Gray74";					//#bdbdbd 
		public static string	Gray75 					= "Gray75";					//#bfbfbf 
		public static string	Gray76 					= "Gray76";					//#c2c2c2 
		public static string	Gray77 					= "Gray77";					//#c4c4c4 
		public static string	Gray78 					= "Gray78";					//#c7c7c7 
		public static string	Gray79 					= "Gray79";					//#c9c9c9 
		public static string	Gray80 					= "Gray80";					//#cccccc 
		public static string	Gray81 					= "Gray81";					//#cfcfcf 
		public static string	Gray82 					= "Gray82";					//#d1d1d1 
		public static string	Gray83 					= "Gray83";					//#d4d4d4 
		public static string	Gray84 					= "Gray84";					//#d6d6d6 
		public static string	Gray85 					= "Gray85";					//#d9d9d9 
		public static string	Gray86 					= "Gray86";					//#dbdbdb 
		public static string	Gray87 					= "Gray87";					//#dedede 
		public static string	Gray88 					= "Gray88";					//#e0e0e0 
		public static string	Gray89 					= "Gray89";					//#e3e3e3 
		public static string	Gray90 					= "Gray90";					//#e5e5e5 
		public static string	Gray91 					= "Gray91";					//#e8e8e8 
		public static string	Gray92 					= "Gray92";					//#ebebeb 
		public static string	Gray93 					= "Gray93";					//#ededed 
		public static string	Gray94 					= "Gray94";					//#f0f0f0 
		public static string	Gray95 					= "Gray95";					//#f2f2f2 
		public static string	Gray96 					= "Gray96";					//#f5f5f5 
		public static string	Gray97 					= "Gray97";					//#f7f7f7 
		public static string	Gray98 					= "Gray98";					//#fafafa 
		public static string	Gray99 					= "Gray99";					//#fcfcfc 
		public static string	Gray100 				= "Gray100";				//#ffffff
		
		/// <summary>Determine the color map index if the indicatewd color name.</summary>
		/// <param name="s">The color name to determine the index for.<see cref="System.String"/></param>
		/// <returns>The color map index on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public static int IndexOf (string s)
		{
			s = s.Trim ().ToLower ();
			for (int index = 0; index <  ColorMap.Length; index++)
			{
				if (ColorMap[index].Name.ToLower ().Equals (s))
					return index;
			}
			return -1;
		}
		
		public static ColorMapping[] ColorMap = new ColorMapping[]
		{	new ColorMapping (AliceBlue				,0xF0F8FF),
			new ColorMapping (AntiqueWhite			,0xFAEBD7),
			new ColorMapping (Aqua					,0x00FFFF),
			new ColorMapping (Aquamarine			,0x7FFFD4),
			new ColorMapping (Azure					,0xF0FFFF),
			new ColorMapping (Beige					,0xF5F5DC),
			new ColorMapping (Bisque				,0xFFE4C4),
			new ColorMapping (Black					,0x000000),
			new ColorMapping (BlanchedAlmond		,0xFFEBCD),
			new ColorMapping (Blue					,0x0000FF),
			new ColorMapping (BlueViolet			,0x8A2BE2),
			new ColorMapping (Brown					,0xA52A2A),
			new ColorMapping (BurlyWood				,0xDEB887),
			new ColorMapping (CadetBlue				,0x5F9EA0),
			new ColorMapping (Chartreuse			,0x7FFF00),
			new ColorMapping (Chocolate				,0xD2691E),
			new ColorMapping (Coral					,0xFF7F50),
			new ColorMapping (CornflowerBlue		,0x6495ED),
			new ColorMapping (Cornsilk				,0xFFF8DC),
			new ColorMapping (Crimson				,0xDC143C),
			new ColorMapping (Cyan					,0x00FFFF),
			new ColorMapping (DarkBlue				,0x00008B),
			new ColorMapping (DarkCyan				,0x008B8B),
			new ColorMapping (DarkGoldenrod			,0xB8860B),
			new ColorMapping (DarkGray				,0xA9A9A9),
			new ColorMapping (DarkGreen				,0x006400),
			new ColorMapping (DarkKhaki				,0xBDB76B),
			new ColorMapping (DarkMagenta			,0x8B008B),
			new ColorMapping (DarkOliveGreen		,0x556B2F),
			new ColorMapping (DarkOrange			,0xFF8C00),
			new ColorMapping (DarkOrchid			,0x9932CC),
			new ColorMapping (DarkRed				,0x8B0000),
			new ColorMapping (DarkSalmon			,0xE9967A),
			new ColorMapping (DarkSeaGreen			,0x8FBC8F),
			new ColorMapping (DarkSlateBlue			,0x483D8B),
			new ColorMapping (DarkSlateGray			,0x2F4F4F),
			new ColorMapping (DarkTurquoise			,0x00CED1),
			new ColorMapping (DarkViolet			,0x9400D3),
			new ColorMapping (DeepPink				,0xFF1493),
			new ColorMapping (DeepSkyBlue			,0x00BFFF),
			new ColorMapping (DimGray				,0x696969),
			new ColorMapping (DodgerBlue			,0x1E90FF),
			new ColorMapping (FireBrick				,0xB22222),
			new ColorMapping (FloralWhite			,0xFFFAF0),
			new ColorMapping (ForestGreen			,0x228B22),
			new ColorMapping (Fuchsia				,0xFF00FF),
			new ColorMapping (Gainsboro				,0xDCDCDC),
			new ColorMapping (GhostWhite			,0xF8F8FF),
			new ColorMapping (Gold					,0xFFD700),
			new ColorMapping (Goldenrod				,0xDAA520),
			new ColorMapping (Gray					,0x808080),
			new ColorMapping (Green					,0x008000),
			new ColorMapping (GreenYellow			,0xADFF2F),
			new ColorMapping (Honeydew				,0xF0FFF0),
			new ColorMapping (HotPink				,0xFF69B4),
			new ColorMapping (IndianRed				,0xCD5C5C),
			new ColorMapping (Indigo				,0x4B0082),
			new ColorMapping (Ivory					,0xFFFFF0),
			new ColorMapping (Khaki					,0xF0E68C),
			new ColorMapping (Lavender				,0xE6E6FA),
			new ColorMapping (LavenderBlush			,0xFFF0F5),
			new ColorMapping (LawnGreen				,0x7CFC00),
			new ColorMapping (LemonChiffon			,0xFFFACD),
			new ColorMapping (LightBlue				,0xADD8E6),
			new ColorMapping (LightCoral			,0xF08080),
			new ColorMapping (LightCyan				,0xE0FFFF),
			new ColorMapping (LightGoldenrodYellow	,0xFAFAD2),
			new ColorMapping (LightGreen			,0x90EE90),
			new ColorMapping (LightGrey				,0xD3D3D3),
			new ColorMapping (LightPink				,0xFFB6C1),
			new ColorMapping (LightSalmon			,0xFFA07A),
			new ColorMapping (LightSeaGreen			,0x20B2AA),
			new ColorMapping (LightSkyBlue			,0x87CEFA),
			new ColorMapping (LightSlateGray		,0x778899),
			new ColorMapping (LightSteelBlue		,0xB0C4DE),
			new ColorMapping (LightYellow			,0xFFFFE0),
			new ColorMapping (Lime					,0x00FF00),
			new ColorMapping (LimeGreen				,0x32CD32),
			new ColorMapping (Linen					,0xFAF0E6),
			new ColorMapping (Magenta				,0xFF00FF),
			new ColorMapping (Maroon				,0x800000),
			new ColorMapping (MediumAquamarine		,0x66CDAA),
			new ColorMapping (MediumBlue			,0x0000CD),
			new ColorMapping (MediumOrchid			,0xBA55D3),
			new ColorMapping (MediumPurple			,0x9370DB),
			new ColorMapping (MediumSeaGreen		,0x3CB371),
			new ColorMapping (MediumSlateBlue		,0x7B68EE),
			new ColorMapping (MediumSpringGreen		,0x00FA9A),
			new ColorMapping (MediumTurquoise		,0x48D1CC),
			new ColorMapping (MediumVioletRed		,0xC71585),
			new ColorMapping (MidnightBlue			,0x191970),
			new ColorMapping (MintCream				,0xF5FFFA),
			new ColorMapping (MistyRose				,0xFFE4E1),
			new ColorMapping (Moccasin				,0xFFE4B5),
			new ColorMapping (NavajoWhite			,0xFFDEAD),
			new ColorMapping (Navy					,0x000080),
			new ColorMapping (OldLace				,0xFDF5E6),
			new ColorMapping (Olive					,0x808000),
			new ColorMapping (OliveDrab				,0x6B8E23),
			new ColorMapping (Orange				,0xFFA500),
			new ColorMapping (OrangeRed				,0xFF4500),
			new ColorMapping (Orchid				,0xDA70D6),
			new ColorMapping (PaleGoldenrod			,0xEEE8AA),
			new ColorMapping (PaleGreen				,0x98FB98),
			new ColorMapping (PaleTurquoise			,0xAFEEEE),
			new ColorMapping (PaleVioletRed			,0xDB7093),
			new ColorMapping (PapayaWhip			,0xFFEFD5),
			new ColorMapping (PeachPuff				,0xFFDAB9),
			new ColorMapping (Peru					,0xCD853F),
			new ColorMapping (Pink					,0xFFC0CB),
			new ColorMapping (Plum					,0xDDA0DD),
			new ColorMapping (PowderBlue			,0xB0E0E6),
			new ColorMapping (Purple				,0x800080),
			new ColorMapping (Red					,0xFF0000),
			new ColorMapping (RosyBrown				,0xBC8F8F),
			new ColorMapping (RoyalBlue				,0x4169E1),
			new ColorMapping (SaddleBrown			,0x8B4513),
			new ColorMapping (Salmon				,0xFA8072),
			new ColorMapping (SandyBrown			,0xF4A460),
			new ColorMapping (SeaGreen				,0x2E8B57),
			new ColorMapping (Seashell				,0xFFF5EE),
			new ColorMapping (Sienna				,0xA0522D),
			new ColorMapping (Silver				,0xC0C0C0),
			new ColorMapping (SkyBlue				,0x87CEEB),
			new ColorMapping (SlateBlue				,0x6A5ACD),
			new ColorMapping (SlateGray				,0x708090),
			new ColorMapping (Snow					,0xFFFAFA),
			new ColorMapping (SpringGreen			,0x00FF7F),
			new ColorMapping (SteelBlue				,0x4682B4),
			new ColorMapping (Tan					,0xD2B48C),
			new ColorMapping (Teal					,0x008080),
			new ColorMapping (Thistle				,0xD8BFD8),
			new ColorMapping (Tomato				,0xFF6347),
			new ColorMapping (Turquoise				,0x40E0D0),
			new ColorMapping (Violet				,0xEE82EE),
			new ColorMapping (Wheat					,0xF5DEB3),
			new ColorMapping (White					,0xFFFFFF),
			new ColorMapping (WhiteSmoke			,0xF5F5F5),
			new ColorMapping (Yellow				,0xFFFF00),
			new ColorMapping (YellowGreen			,0x9ACD32),
			
			new ColorMapping (Gray0 				,0x000000),
			new ColorMapping (Gray1 				,0x030303),
			new ColorMapping (Gray2 				,0x050505),
			new ColorMapping (Gray3 				,0x080808),
			new ColorMapping (Gray4 				,0x0a0a0a),
			new ColorMapping (Gray5 				,0x0d0d0d),
			new ColorMapping (Gray6 				,0x0f0f0f),
			new ColorMapping (Gray7 				,0x121212),
			new ColorMapping (Gray8 				,0x141414),
			new ColorMapping (Gray9 				,0x171717),
			new ColorMapping (Gray10 				,0x1A1A1A),
			new ColorMapping (Gray11 				,0x1c1c1c),
			new ColorMapping (Gray12 				,0x1f1f1f),
			new ColorMapping (Gray13 				,0x212121),
			new ColorMapping (Gray14 				,0x242424),
			new ColorMapping (Gray15 				,0x262626),
			new ColorMapping (Gray16 				,0x292929),
			new ColorMapping (Gray17 				,0x2b2b2b),
			new ColorMapping (Gray18 				,0x2e2e2e),
			new ColorMapping (Gray19 				,0x303030),
			new ColorMapping (Gray20 				,0x333333),
			new ColorMapping (Gray21 				,0x363636),
			new ColorMapping (Gray22 				,0x383838),
			new ColorMapping (Gray23 				,0x3b3b3b),
			new ColorMapping (Gray24 				,0x3d3d3d),
			new ColorMapping (Gray25 				,0x404040),
			new ColorMapping (Gray26 				,0x424242),
			new ColorMapping (Gray27 				,0x454545),
			new ColorMapping (Gray28 				,0x474747),
			new ColorMapping (Gray29 				,0x4a4a4a),
			new ColorMapping (Gray30 				,0x4d4d4d),
			new ColorMapping (Gray31 				,0x4f4f4f),
			new ColorMapping (Gray32 				,0x525252),
			new ColorMapping (Gray33 				,0x545454),
			new ColorMapping (Gray34 				,0x575757),
			new ColorMapping (Gray35 				,0x595959),
			new ColorMapping (Gray36 				,0x5c5c5c),
			new ColorMapping (Gray37 				,0x5e5e5e),
			new ColorMapping (Gray38 				,0x616161),
			new ColorMapping (Gray39 				,0x636363),
			new ColorMapping (Gray40 				,0x666666),
			new ColorMapping (Gray41 				,0x696969),
			new ColorMapping (Gray42 				,0x6b6b6b),
			new ColorMapping (Gray43 				,0x6e6e6e),
			new ColorMapping (Gray44 				,0x707070),
			new ColorMapping (Gray45 				,0x737373),
			new ColorMapping (Gray46 				,0x757575),
			new ColorMapping (Gray47 				,0x787878),
			new ColorMapping (Gray48 				,0x7a7a7a),
			new ColorMapping (Gray49 				,0x7d7d7d),
			new ColorMapping (Gray50 				,0x7f7f7f),
			new ColorMapping (Gray51 				,0x828282),
			new ColorMapping (Gray52 				,0x858585),
			new ColorMapping (Gray53 				,0x878787),
			new ColorMapping (Gray54 				,0x8a8a8a),
			new ColorMapping (Gray55 				,0x8c8c8c),
			new ColorMapping (Gray56 				,0x8f8f8f),
			new ColorMapping (Gray57 				,0x919191),
			new ColorMapping (Gray58 				,0x949494),
			new ColorMapping (Gray59 				,0x969696),
			new ColorMapping (Gray60 				,0x999999),
			new ColorMapping (Gray61 				,0x9c9c9c),
			new ColorMapping (Gray62 				,0x9e9e9e),
			new ColorMapping (Gray63 				,0xA1A1A1),
			new ColorMapping (Gray64 				,0xa3a3a3),
			new ColorMapping (Gray65 				,0xa6a6a6),
			new ColorMapping (Gray66 				,0xa8a8a8),
			new ColorMapping (Gray67 				,0xababab),
			new ColorMapping (Gray68 				,0xadadad),
			new ColorMapping (Gray69 				,0xb0b0b0),
			new ColorMapping (Gray70 				,0xb3b3b3),
			new ColorMapping (Gray71 				,0xb5b5b5),
			new ColorMapping (Gray72 				,0xb8b8b8),
			new ColorMapping (Gray73 				,0xbababa),
			new ColorMapping (Gray74 				,0xbdbdbd),
			new ColorMapping (Gray75 				,0xbfbfbf),
			new ColorMapping (Gray76 				,0xc2c2c2),
			new ColorMapping (Gray77 				,0xc4c4c4),
			new ColorMapping (Gray78 				,0xc7c7c7),
			new ColorMapping (Gray79 				,0xc9c9c9),
			new ColorMapping (Gray80 				,0xcccccc),
			new ColorMapping (Gray81 				,0xcfcfcf),
			new ColorMapping (Gray82 				,0xd1d1d1),
			new ColorMapping (Gray83 				,0xd4d4d4),
			new ColorMapping (Gray84 				,0xd6d6d6),
			new ColorMapping (Gray85 				,0xd9d9d9),
			new ColorMapping (Gray86 				,0xdbdbdb),
			new ColorMapping (Gray87 				,0xdedede),
			new ColorMapping (Gray88 				,0xe0e0e0),
			new ColorMapping (Gray89 				,0xe3e3e3),
			new ColorMapping (Gray90 				,0xe5e5e5),
			new ColorMapping (Gray91 				,0xe8e8e8),
			new ColorMapping (Gray92 				,0xebebeb),
			new ColorMapping (Gray93 				,0xededed),
			new ColorMapping (Gray94 				,0xf0f0f0),
			new ColorMapping (Gray95 				,0xf2f2f2),
			new ColorMapping (Gray96 				,0xf5f5f5),
			new ColorMapping (Gray97 				,0xf7f7f7),
			new ColorMapping (Gray98 				,0xfafafa),
			new ColorMapping (Gray99 				,0xfcfcfc),
			new ColorMapping (Gray100 				,0xffffff)		};
	}
}