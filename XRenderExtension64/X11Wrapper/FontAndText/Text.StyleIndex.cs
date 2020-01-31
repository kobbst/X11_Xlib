// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: February 2015
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2015 Steffen Ploetz
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

namespace X11.Text
{

    /// <summary>Style index mask (32 styles) for Char structure's style.</summary>
    [Flags]
    public enum StyleIndex : uint
    {
        None    = 0x00000000,
        Style0  = 0x00000001,
        Style1  = 0x00000002,
        Style2  = 0x00000004,
        Style3  = 0x00000008,
        Style4  = 0x00000010,
        Style5  = 0x00000020,
        Style6  = 0x00000040,
        Style7  = 0x00000080,
        Style8  = 0x00000100,
        Style9  = 0x00000200,
        Style10 = 0x00000400,
        Style11 = 0x00000800,
        Style12 = 0x00001000,
        Style13 = 0x00002000,
        Style14 = 0x00004000,
        Style15 = 0x00008000,
        Style16 = 0x00010000,
        Style17 = 0x00020000,
        Style18 = 0x00040000,
        Style19 = 0x00080000,
        Style20 = 0x00100000,
        Style21 = 0x00200000,
        Style22 = 0x00400000,
        Style23 = 0x00800000,
        Style24 = 0x01000000,
        Style25 = 0x02000000,
        Style26 = 0x04000000,
        Style27 = 0x08000000,
        Style28 = 0x10000000,
        Style29 = 0x20000000,
        Style30 = 0x40000000,
        Style31 = 0x80000000,
        All     = 0xFFFFFFFF
    }

}