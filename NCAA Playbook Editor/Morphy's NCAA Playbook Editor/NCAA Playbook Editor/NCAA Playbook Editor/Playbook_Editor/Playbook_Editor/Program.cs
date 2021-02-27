/******************************************************************************
 * M20PlaybookEditor v1.14
 * Copyright (C) 2019 Chris Johns
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace Playbook_Editor
{
    #region Classes

    public class PSALroute
    {
        public class Steps
        {
            public int rec { get; set; }
            public int code { get; set; }
            public int val1 { get; set; }
            public int val2 { get; set; }
            public int val3 { get; set; }
            public int PSAL { get; set; }
            public int step { get; set; }
            public int PLRR { get; set; }
            public int ARTL { get; set; }

            public override string ToString()
            {
                return
                    "Rec#: " + rec +
                    "   code: " + code +
                    "   val1: " + val1 +
                    "   val2: " + val2 +
                    "   val3: " + val3 +
                    "   PSAL: " + PSAL +
                    "   step: " + step +
                    "   PLRR: " + PLRR;
            }

            public Steps DeepCopy()
            {
                Steps other = (Steps)MemberwiseClone();
                return other;
            }

            public Steps Clone()
            {
                return new Steps
                {
                    rec = rec,
                    code = code,
                    val1 = val1,
                    val2 = val2,
                    val3 = val3,
                    PSAL = PSAL,
                    step = step,
                    PLRR = PLRR,
                    ARTL = ARTL
                };
            }
        }

        public string CPFMtype;
        public bool Highlighted;
        public string Position;
        public int ID;
        public int PLRR;
        public int ARTL;
        public PSALColor RouteColor = PSALColor.Undefined;
        public double AngleRatio = 0.35556;
        public XY playerXY;
        public int fmtx, artx, fmty, arty;
        public List<SETP> SETP;
        public List<Steps> steps;
        public List<XY> route;
        public List<XY> routeOption1;
        public List<XY> routeOption2;
        public ARTL aRTL;
        public PointF zoneXY { get; set; }
        public PointF zoneSize { get; set; }
        public GraphicsPath ARTLroute { get; set; }
        public GraphicsPath ARTLrouteOption1 { get; set; }
        public GraphicsPath ARTLrouteOption2 { get; set; }
        public SRFT SRFT { get; set; }

        public PSALroute DeepCopy()
        {
            PSALroute other = (PSALroute)MemberwiseClone();
            return other;
        }

        public PSALroute Clone()
        {
            return new PSALroute
            {
                CPFMtype = CPFMtype,
                Highlighted = Highlighted,
                Position = Position,
                ID = ID,
                PLRR = PLRR,
                ARTL = ARTL,
                RouteColor = RouteColor,
                AngleRatio = AngleRatio,
                playerXY = playerXY,
                fmtx = fmtx,
                artx = artx,
                fmty = fmty,
                arty = arty,
                SETP = SETP.ConvertAll(setp => setp.Clone()),
                steps = steps.ConvertAll(step => step.Clone()),
                route = route.ConvertAll(route => route.Clone()),
                routeOption1 = routeOption1.ConvertAll(route => route.Clone()),
                routeOption2 = routeOption2.ConvertAll(route => route.Clone()),
                aRTL = aRTL
            };
        }
    }

    public class XY
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return
                " { " + X +
                ", " + Y +
                " }";
        }

        public XY Clone()
        {
            return new XY
            {
                X = X,
                Y = Y
            };
        }
    }

    public class ID
    {
        public ushort id { get; set; }
        public string Name { get; set; }
    }

    public class Formation
    {
        public ID FORM { get; set; }
        public ID SETL { get; set; }
        public Point poso0 { get; set; }
        public Point poso1 { get; set; }
        public Point poso2 { get; set; }
        public Point poso3 { get; set; }
        public Point poso4 { get; set; }
        public Point poso5 { get; set; }
        public Point poso6 { get; set; }
        public Point poso7 { get; set; }
        public Point poso8 { get; set; }
        public Point poso9 { get; set; }
        public Point poso10 { get; set; }
    }

    public class TableNames
    {
        public int rec { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return "Rec#: " + rec + "   Name: " + name;
        }
    }

    public class CPFM
    {
        public int rec { get; set; }
        public int FORM { get; set; }
        public int FTYP { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   FORM: " + FORM +
                "   FTYP: " + FTYP +
                "   Name: " + name;
        }
    }

    public static class CPFMtype
    {
        public static IDictionary<int, string> PLRRnameDic = new Dictionary<int, string>()
        {
            { 1, "Offense" },
            { 2, "Offense" },
            { 3, "Offense" },
            { 11, "Defense" },
            { 12, "Defense" },
            { 13, "Defense" }
        };
    }

    public class SETL
    {
        public int rec { get; set; }
        public int setl { get; set; }
        public int FORM { get; set; }
        public int MOTN { get; set; }
        public int CLAS { get; set; }
        public int SETT { get; set; }
        public int SITT { get; set; }
        public int SLF_ { get; set; }
        public string name { get; set; }
        public int poso { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + setl +
                "   FORM: " + FORM +
                "   MOTN: " + MOTN +
                "   CLAS: " + CLAS +
                "   SETT: " + SETT +
                "   SITT: " + SITT +
                "   SLF_: " + SLF_ +
                "   Name: " + name +
                "   poso: " + poso;
        }

        public SETL Clone()
        {
            return new SETL
            {
                rec = rec,
                setl = setl,
                FORM = FORM,
                MOTN = MOTN,
                CLAS = CLAS,
                SETT = SETT,
                SITT = SITT,
                SLF_ = SLF_,
                name = name,
                poso = poso
            };
        }
    }

    public class PBPL
    {
        public int rec { get; set; }
        public int COMF { get; set; }
        public int SETL { get; set; }
        public int PLYL { get; set; }
        public int SRMM { get; set; }
        public int SITT { get; set; }
        public int PLYT { get; set; }
        public int PLF_ { get; set; }
        public string name { get; set; }
        public int risk { get; set; }
        public int motn { get; set; }
        public int vpos { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   COMF: " + COMF +
                "   SETL: " + SETL +
                "   PLYL: " + PLYL +
                "   SRMM: " + SRMM +
                "   SITT: " + SITT +
                "   PLYT: " + PLYT +
                "   PLF_: " + PLF_ +
                "   Name: " + name +
                "   risk: " + risk +
                "   motn: " + motn +
                "   vpos: " + vpos;
        }

        public PBPL DeepCopy()
        {
            PBPL other = (PBPL)MemberwiseClone();
            return other;
        }

        public PBPL Clone()
        {
            return new PBPL
            {
                rec = rec,
                COMF = COMF,
                SETL = SETL,
                PLYL = PLYL,
                SRMM = SRMM,
                SITT = SITT,
                PLYT = PLYT,
                PLF_ = PLF_,
                name = name,
                risk = risk,
                motn = motn,
                vpos = vpos
            };
        }
    }

    public class PLPD
    {
        public int rec { get; set; }
        public int com1 { get; set; }
        public int con1 { get; set; }
        public int per1 { get; set; }
        public int rcv1 { get; set; }
        public int com2 { get; set; }
        public int con2 { get; set; }
        public int per2 { get; set; }
        public int rcv2 { get; set; }
        public int com3 { get; set; }
        public int con3 { get; set; }
        public int per3 { get; set; }
        public int rcv3 { get; set; }
        public int com4 { get; set; }
        public int con4 { get; set; }
        public int per4 { get; set; }
        public int rcv4 { get; set; }
        public int com5 { get; set; }
        public int con5 { get; set; }
        public int per5 { get; set; }
        public int rcv5 { get; set; }
        public int PLYL { get; set; }

        public PLPD DeepCopy()
        {
            PLPD other = (PLPD)MemberwiseClone();
            return other;
        }

        public PLPD Clone()
        {
            return new PLPD
            {
                rec = rec,
                com1 = com1,
                con1 = con1,
                per1 = per1,
                rcv1 = rcv1,
                com2 = com2,
                con2 = con2,
                per2 = per2,
                rcv2 = rcv2,
                com3 = com3,
                con3 = con3,
                per3 = per3,
                rcv3 = rcv3,
                com4 = com4,
                con4 = con4,
                per4 = per4,
                rcv4 = rcv4,
                com5 = com5,
                con5 = con5,
                per5 = per5,
                rcv5 = rcv5,
                PLYL = PLYL
            };
        }
    }

    public class PLRD
    {
        public int rec { get; set; }
        public int PLYL { get; set; }
        public int hole { get; set; }

        public PLRD DeepCopy()
        {
            PLRD other = (PLRD)MemberwiseClone();
            return other;
        }

        public PLRD Clone()
        {
            return new PLRD
            {
                rec = rec,
                PLYL = PLYL,
                hole = hole
            };
        }
    }

    public class PLYS : IEquatable<PLYS>
    {
        public int rec { get; set; }
        public string Position { get; set; }
        public int PSAL { get; set; }
        public int ARTL { get; set; }
        public int PLYL { get; set; }
        public int PLRR { get; set; }
        public int poso { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   Position: " + Position +
                "   PSAL: " + PSAL +
                "   ARTL: " + ARTL +
                "   PLYL: " + PLYL +
                "   PLRR: " + PLRR +
                "   poso: " + poso;
        }

        public bool Equals(PLYS other)
        {
            //Check whether the compared object is null. 
            if (ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (ReferenceEquals(this, other)) return true;

            //Check whether the PSAL IDs are equal. 
            return PSAL.Equals(other.PSAL);
        }

        // If Equals() returns true for a pair of objects then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {
            //Get hash code for the Code field. 
            int hashPSALID = PSAL.GetHashCode();

            //Calculate the hash code for the product. 
            return hashPSALID;
        }

        public PLYS DeepCopy()
        {
            PLYS other = (PLYS)MemberwiseClone();
            return other;
        }

        public PLYS Clone()
        {
            return new PLYS
            {
                rec = rec,
                Position = Position,
                PSAL = PSAL,
                ARTL = ARTL,
                PLYL = PLYL,
                PLRR = PLRR,
                poso = poso
            };
        }
    }

    public class SETP
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int setp { get; set; }
        public int SGT_ { get; set; }
        public int arti { get; set; }
        public int opnm { get; set; }
        public int tabo { get; set; }
        public int poso { get; set; }
        public int odep { get; set; }
        public int flas { get; set; }
        public int DPos { get; set; }
        public int EPos { get; set; }
        public int fmtx { get; set; }
        public int artx { get; set; }
        public int fmty { get; set; }
        public int arty { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL +
                "   setp: " + setp +
                "   SGT_: " + SGT_ +
                "   arti: " + arti +
                "   opnm: " + opnm +
                "   tabo: " + tabo +
                "   poso: " + poso +
                "   odep: " + odep +
                "   flas: " + flas +
                "   DPos: " + DPos +
                "   EPos: " + EPos +
                "   fmtx: " + fmtx +
                "   artx: " + artx +
                "   fmty: " + fmty +
                "   arty: " + arty;
        }

        public SETP DeepCopy()
        {
            SETP other = (SETP)MemberwiseClone();
            return other;
        }

        public SETP Clone()
        {
            return new SETP
            {
                rec = rec,
                SETL = SETL,
                setp = setp,
                SGT_ = SGT_,
                arti = arti,
                opnm = opnm,
                tabo = tabo,
                poso = poso,
                odep = odep,
                flas = flas,
                DPos = DPos,
                EPos = EPos,
                fmtx = fmtx,
                artx = artx,
                fmty = fmty,
                arty = arty
            };
        }
    }

    public class SETG
    {
        public int rec { get; set; }
        public int SETP { get; set; }
        public int SGF_ { get; set; }
        public int SF__ { get; set; }
        public float x___ { get; set; }
        public float y___ { get; set; }
        public float fx__ { get; set; }
        public float fy__ { get; set; }
        public int anm_ { get; set; }
        public int dir_ { get; set; }
        public int fanm { get; set; }
        public int fdir { get; set; }
        public bool active { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETP: " + SETP +
                "   SGF_: " + SGF_ +
                "   SF__: " + SF__ +
                "   x___: " + x___ +
                "   y___: " + y___ +
                "   fx__: " + fx__ +
                "   fy__: " + fy__ +
                "   anm_: " + anm_ +
                "   dir_: " + dir_ +
                "   fanm: " + fanm +
                "   fdir: " + fdir +
                "   active: " + active;
        }

        public SETG DeepCopy()
        {
            SETG other = (SETG)MemberwiseClone();
            return other;
        }

        public SETG Clone()
        {
            return new SETG
            {
                rec = rec,
                SETP = SETP,
                SGF_ = SGF_,
                SF__ = SF__,
                x___ = x___,
                y___ = y___,
                fx__ = fx__,
                fy__ = fy__,
                anm_ = anm_,
                dir_ = dir_,
                fanm = fanm,
                fdir = fdir,
                active = active
            };
        }
    }

    public class SGFF
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int SGF_ { get; set; }
        public string name { get; set; }
        public int dflt { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL +
                "   SGF_: " + SGF_ +
                "   Name: " + name +
                "   dflt: " + dflt;
        }

        public SGFF DeepCopy()
        {
            SGFF other = (SGFF)MemberwiseClone();
            return other;
        }

        public SGFF Clone()
        {
            return new SGFF
            {
                rec = rec,
                SETL = SETL,
                SGF_ = SGF_,
                name = name,
                dflt = dflt
            };
        }
    }

    public struct PLAY
    {
        public List<SETL> SETL { get; set; }
        public List<PBPL> PBPL { get; set; }
        public List<PLPD> PLPD { get; set; }
        public List<PLRD> PLRD { get; set; }
        public List<PLYS> PLYS { get; set; }
        public List<SETP> SETP { get; set; }
        public List<SETG> SETG { get; set; }
        public List<SGFF> SGFF { get; set; }
        public List<SRFT> SRFT { get; set; }
        public PSALroute[] posos { get; set; }

        public PLAY DeepCopy()
        {
            PLAY other = (PLAY)MemberwiseClone();
            return other;
        }

        public PLAY Clone()
        {
            return new PLAY
            {
                SETL = SETL.ConvertAll(item => item.Clone()),
                PBPL = PBPL.ConvertAll(item => item.Clone()),
                PLPD = PLPD.ConvertAll(item => item.Clone()),
                PLRD = PLRD.ConvertAll(item => item.Clone()),
                PLYS = PLYS.ConvertAll(item => item.Clone()),
                SETP = SETP.ConvertAll(item => item.Clone()),
                SETG = SETG.ConvertAll(item => item.Clone()),
                SGFF = SGFF.ConvertAll(item => item.Clone()),
                // SRFT = SRFT.ConvertAll(item => item.Clone()),
                posos = (PSALroute[]) posos.Clone()
            };
        }
    }

    public class PSAL : IEquatable<PSAL>
    {
        public int rec { get; set; }
        public int val1 { get; set; }
        public int val2 { get; set; }
        public int val3 { get; set; }
        public int psal { get; set; }
        public int code { get; set; }
        public int step { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   val1: " + val1 +
                "   val2: " + val2 +
                "   val3: " + val3 +
                "   psal: " + psal +
                "   code: " + code +
                "   step: " + step;
        }

        public bool Equals(PSAL other)
        {
            //Check whether the compared object is null. 
            if (ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (ReferenceEquals(this, other)) return true;

            //Check whether the PSAL IDs are equal. 
            return psal.Equals(other.psal);
        }

        // If Equals() returns true for a pair of objects then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {
            //Get hash code for the Code field. 
            int hashPSALID = psal.GetHashCode();

            //Calculate the hash code for the product. 
            return hashPSALID;
        }

        public PSAL Clone()
        {
            return new PSAL
            {
                rec = rec,
                val1 = val1,
                val2 = val2,
                val3 = val3,
                psal = psal,
                code = code,
                step = step
            };
        }
    }

    public class PLRR : IEquatable<PLRR>
    {
        public int psal { get; set; }
        public int plrr { get; set; }
        public int artl { get; set; }
        public string type { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "psal: " + psal +
                "   plrr: " + plrr +
                "   artl: " + artl +
                "   type: " + type +
                "   name: " + name;
        }

        public bool Equals(PLRR other)
        {
            //Check whether the compared object is null. 
            if (ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (ReferenceEquals(this, other)) return true;

            //Check whether the PSAL IDs are equal. 
            return plrr.Equals(other.plrr);
        }

        // If Equals() returns true for a pair of objects then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {
            //Get hash code for the Code field. 
            int hashPSALID = plrr.GetHashCode();

            //Calculate the hash code for the product. 
            return hashPSALID;
        }
    }

    public class SRFT
    {
        public int rec { get; set; }
        public int SIDE { get; set; }
        public int YOFF { get; set; }
        public int TECH { get; set; }
        public int PLYL { get; set; }
        public int STAN { get; set; }
        public int PLYR { get; set; }
        public int PRIS { get; set; }
        public int GAPS { get; set; }
        public int ASSS { get; set; }
        public int PRIW { get; set; }
        public int GAPW { get; set; }
        public int ASSW { get; set; }

        public override string ToString()
        {
            return
                "rec: " + rec +
                "   SIDE: " + SIDE +
                "   YOFF: " + YOFF +
                "   TECH: " + TECH +
                "   PLYL: " + PLYL +
                "   STAN: " + STAN +
                "   PLYR: " + PLYR +
                "   PRIS: " + PRIS +
                "   GAPS: " + GAPS +
                "   ASSS: " + ASSS +
                "   PRIW: " + PRIW +
                "   GAPW: " + GAPW +
                "   ASSW: " + ASSW;
        }

        public SRFT Clone()
        {
            return new SRFT
            {
                rec = rec,
                SIDE = SIDE,
                YOFF = YOFF,
                TECH = TECH,
                PLYL = PLYL,
                STAN = STAN,
                PLYR = PLYR,
                PRIS = PRIS,
                GAPS = GAPS,
                ASSS = ASSS,
                PRIW = PRIW,
                GAPW = GAPW,
                ASSW = ASSW
            };
        }
    }

    public static class PLRRname
    {
        public static IDictionary<int, string> PLRRnameDic = new Dictionary<int, string>()
        {
            { 1, "Pass Block" },
            { 2, "Run Block" },
            { 3, "Blitz" },
            { 5, "DL Rush" },
            { 6, "QB Spy" },
            { 7, "Flat (Left)" },
            { 8, "Flat (Right)" },
            { 9, "Deep Half (Left)" },
            { 10, "Deep Half (Right)" },
            { 11, "Deep Quarter (Inside Left)" },
            { 12, "Deep Quarter (Inside Right)" },
            { 13, "Deep Quarter (Outside Left)" },
            { 14, "Deep Quarter (Outside Right)" },
            { 15, "Deep Third (Left)" },
            { 16, "Deep Third (Middle)" },
            { 17, "Deep Third (Right)" },
            { 18, "Flat (Left)" },
            { 19, "Flat (Right)" },
            { 20, "Hook/Flat" },
            { 21, "Hook/Seam (Left)" },
            { 22, "Hook/Seam (Right)" },
            { 24, "Field Goal" },
            { 25, "Field Goal Shovel" },
            { 26, "Block" },
            { 27, "Onside Kick" },
            { 28, "Squib Kick" },
            { 29, "Punt" },
            { 30, "Fake Punt Pass" },
            { 31, "Fake Punt Sweep" },
            { 32, "QB Run" },
            { 33, "Fake FG Shovel" },
            { 34, "Fake FG Holder Blast" },
            { 35, "Fake Spike" },
            { 36, "Fake FG Flip Pass" },
            { 37, "Fake FG Direct Snap" },
            { 38, "QB Kneel" },
            { 39, "QB Option" },
            { 40, "QB Drop-Back" },
            { 41, "QB Play Action" },
            { 43, "QB Option" },
            { 44, "QB Shovel / Speed Option" },
            { 45, "Spike Ball" },
            { 47, "HB Run Power O" },
            { 48, "HB Run Dive" },
            { 49, "HB Run Draw" },
            { 50, "HB Run Draw Bunnle" },
            { 52, "Fake Punt (FB Off-Tackle)" },
            { 53, "HB Run Triple Option" },
            { 54, "HB Run Read Option" },
            { 55, "HB Pass (Philly Special)" },
            { 56, "HB Run Zone" },
            { 57, "HB Run Stretch" },
            { 58, "HB Run Toss" },
            { 60, "PoCo / CoPo" },
            { 61, "Comeback" },
            { 62, "Post / Corner" },
            { 63, "Slant" },
            { 64, "PoCo / CoPo" },
            { 65, "Deep Curl" },
            { 66, "Post / Corner" },
            { 67, "Cross" },
            { 68, "Curl" },
            { 69, "Curl" },
            { 70, "Curl'N'Go" },
            { 71, "Drag" },
            { 72, "Flat Left" },
            { 73, "Flat Right" },
            { 74, "Hitch" },
            { 75, "Hitch'N'Go" },
            { 76, "In / Out" },
            { 77, "In / Out" },
            { 78, "Out'N'Go / In'N'Go" },
            { 79, "Short In / Out" },
            { 80, "Option" },
            { 81, "In / Out" },
            { 82, "In / Out" },
            { 83, "Out'N'Up / In'N'Up" },
            { 85, "Short In / Short Out" },
            { 86, "PoCo / CoPo" },
            { 87, "Post / Corner / PoCo / CoPo" },
            { 88, "Post / Corner" },
            { 89, "Post Sit / Corner Sit" },
            { 90, "HB Angle" },
            { 91, "HB Out / In" },
            { 92, "HB Out / In" },
            { 93, "Screen" },
            { 94, "HB Flat Left" },
            { 95, "HB Flat Right" },
            { 96, "Short Slant" },
            { 97, "Sluggo" },
            { 98, "Streak / Fade / Seam" },
            { 99, "Orbit" },
            { 100, "HB Swing Right" },
            { 101, "HB Wheel" },
            { 102, "Wheel" },
            { 103, "Trail/Shake" },
            { 104, "Whip" },
            { 105, "Kick Return Blocking" },
            { 106, "Kickoff" },
            { 107, "Punt Return" },
            { 108, "Punt Return Blocking" },
            { 111, "QB Drop-back" },
            { 112, "QB Sneak" },
            { 113, "QB Toss" },
            { 115, "Delay Left" },
            { 116, "Delay Right" },
            { 118, "Delay Go" },
            { 119, "Delay Right" },
            { 121, "Delay Right" },
            { 123, "HB Delay Hitch" },
            { 124, "HB Delay Hitch" },
            { 125, "HB Delay Angle" },
            { 126, "Spot/Sit" },
            { 127, "Punt" },
            { 130, "Jet Sweep/End-Around" },
            { 131, "Fake End-Around" },
            { 133, "Man Coverage (Left Outside WR / WR1)" },
            { 134, "Man Coverage (TE / WR4)" },
            { 135, "Man Coverage (RB / WR5)" },
            { 136, "Man Coverage (Slot WR / TE2 / FB)" },
            { 137, "Man Coverage (Right Outside WR / WR2)" },
            { 138, "Onside Kick" },
            { 139, "Onside Recover" },
            { 141, "Goalline Fade" }
        };
    }

    public class ARTL
    {
        public override string ToString()
        {
            return
                "rec: " + rec +
                "   artl: " + artl +
                "   acnt: " + acnt + "\n" +

                "   sp: " + "[" + string.Join(", ", sp) + "]\n" +
                "   ls: " + "[" + string.Join(", ", ls) + "]\n" +
                "   at: " + "[" + string.Join(", ", at) + "]\n" +
                "   ct: " + "[" + string.Join(", ", ct) + "]\n" +
                "   lt: " + "[" + string.Join(", ", lt) + "]\n" +
                "   au: " + "[" + string.Join(", ", au) + "]\n" +
                "   av: " + "[" + string.Join(", ", av) + "]\n" +
                "   ax: " + "[" + string.Join(", ", ax) + "]\n" +
                "   ay: " + "[" + string.Join(", ", ay) + "]";
        }

        public int rec { get; set; }
        public int artl { get; set; }
        public int acnt { get; set; }
        
        public int[] sp { get; set; }
        public int[] ls { get; set; }
        public int[] at { get; set; }
        public int[] ct { get; set; }
        public int[] lt { get; set; }
        public int[] au { get; set; }
        public int[] av { get; set; }
        public int[] ax { get; set; }
        public int[] ay { get; set; }

        public static PointF QuarterHookFlat
        {
            get
            {
                return new PointF(32, 13.711f);
            }
        }

        public static PointF DeepThird
        {
            get
            {
                return new PointF(59.0625f, 19.3359375f); ;
            }
        }

        public static PointF DeepHalf
        {
            get
            {
                return new PointF(78.046875f, 21.09375f);
            }
        }

        public static PointF QBSpy
        {
            get
            {
                return new PointF(21.09375f, 13.359375f);
            }
        }
    }

    public class MyRenderer : ToolStripProfessionalRenderer
    {
        public MyRenderer() : base(new MyColors()) { }
    }

    public class MyColors : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Color.Yellow; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(0, Color.Yellow); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.Yellow; }
        }
        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(0, Color.Yellow); }
        }

    }

    public class PSALColor
    {
        public bool Equals(PSALColor other)
        {
            if (other == null) return false;

            return (Type == other.Type);
        }

        public string Type { get; set; }
        public Color Color { get; set; }

        public static Color PlayerIconColor = Color.White;
        public static Color PlayerHighlightColor = Color.FromArgb(255, 64, 0, 255);

        public static PSALColor Undefined
        {
            get
            {
                return new PSALColor() { Type = "Undefined", Color = Color.Empty };
            }
        }

        public static PSALColor Block
        {
            get
            {
                return new PSALColor() { Type = "Block", Color = Color.FromArgb(255, 200, 200, 200) };
            }
        }

        public static PSALColor BaseRoute
        {
            get
            {
                return new PSALColor() { Type = "Base Route", Color = Color.FromArgb(255, 195, 177, 82) };
            }
        }

        public static PSALColor PrimaryRoute
        {
            get
            {
                return new PSALColor() { Type = "Primary Route", Color = Color.FromArgb(255, 219, 65, 85) };
            }
        }

        public static PSALColor DelayRoute
        {
            get
            {
                return new PSALColor() { Type = "Delay Route", Color = Color.FromArgb(255, 52, 111, 247) };
            }
        }

        public static PSALColor MotionRoute
        {
            get
            {
                return new PSALColor() { Type = "Motion Route", Color = Color.FromArgb(255, 156, 227, 241) };
            }
        }

        public static PSALColor Run
        {
            get
            {
                return new PSALColor() { Type = "Run", Color = Color.FromArgb(255, 219, 65, 85) };
            }
        }

        public static PSALColor QBScramble
        {
            get
            {
                return new PSALColor() { Type = "QB Scramble", Color = Color.FromArgb(255, 19, 232, 132) };
            }
        }

        public static PSALColor QBHandoff
        {
            get
            {
                return new PSALColor() { Type = "QB Handoff", Color = Color.FromArgb(255, 219, 65, 85) };
            }
        }

        public static PSALColor Kickoff
        {
            get
            {
                return new PSALColor() { Type = "Kickoff", Color = Color.FromArgb(255, 219, 65, 85) };
            }
        }

        public static PSALColor CloudFlat
        {
            get
            {
                return new PSALColor() { Type = "Cloud Flat", Color = Color.FromArgb(255, 8, 194, 219) };
            }
        }

        public static PSALColor HardFlat
        {
            get
            {
                return new PSALColor() { Type = "Hard Flat", Color = Color.FromArgb(255, 90, 163, 170) };
            }
        }

        public static PSALColor SoftSquat
        {
            get
            {
                return new PSALColor() { Type = "Soft Squat", Color = Color.FromArgb(255, 153, 234, 244) };
            }
        }

        public static PSALColor MidRead
        {
            get
            {
                return new PSALColor() { Type = "Mid Read", Color = Color.FromArgb(255, 165, 189, 107) };
            }
        }

        public static PSALColor ThreeReceiverHook
        {
            get
            {
                return new PSALColor() { Type = "Three Receiver", Color = Color.FromArgb(255, 219, 243, 159) };
            }
        }

        public static PSALColor HookCurl
        {
            get
            {
                return new PSALColor() { Type = "Hook Curl", Color = Color.FromArgb(255, 231, 239, 192) };
            }
        }

        public static PSALColor VertHook
        {
            get
            {
                return new PSALColor() { Type = "Vert Hook", Color = Color.FromArgb(255, 214, 227, 148) };
            }
        }

        public static PSALColor CurlFlat
        {
            get
            {
                return new PSALColor() { Type = "Curl Flat", Color = Color.FromArgb(255, 145, 62, 222) };
            }
        }

        public static PSALColor SeamFlat
        {
            get
            {
                return new PSALColor() { Type = "Seam Flat", Color = Color.FromArgb(255, 189, 128, 245) };
            }
        }

        public static PSALColor QuarterFlat
        {
            get
            {
                return new PSALColor() { Type = "Quarter Flat", Color = Color.FromArgb(255, 255, 182, 247) };
            }
        }

        public static PSALColor DeepZone
        {
            get
            {
                return new PSALColor() { Type = "Deep Zone", Color = Color.FromArgb(255, 49, 109, 247) };
            }
        }

        public static PSALColor QBSpy
        {
            get
            {
                return new PSALColor() { Type = "QB Spy", Color = Color.FromArgb(255, 200, 120, 16) };
            }
        }

        public static PSALColor RushQB
        {
            get
            {
                return new PSALColor() { Type = "Rush QB", Color = Color.FromArgb(255, 219, 65, 85) };
            }
        }
    }

    #endregion

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPlaybook());
        }

        public static Bitmap[] Loading_Football = new Bitmap[]
            {
                Properties.Resources.Football_00,
                Properties.Resources.Football_01,
                Properties.Resources.Football_02,
                Properties.Resources.Football_03,
                Properties.Resources.Football_04,
                Properties.Resources.Football_05,
                Properties.Resources.Football_06,
                Properties.Resources.Football_07,
                Properties.Resources.Football_08,
                Properties.Resources.Football_09,
                Properties.Resources.Football_10,
                Properties.Resources.Football_11,
                Properties.Resources.Football_12,
                Properties.Resources.Football_13,
                Properties.Resources.Football_14,
                Properties.Resources.Football_15,
                Properties.Resources.Football_16,
                Properties.Resources.Football_17,
                Properties.Resources.Football_18,
                Properties.Resources.Football_19,
                Properties.Resources.Football_20,
                Properties.Resources.Football_21,
                Properties.Resources.Football_22,
                Properties.Resources.Football_23,
                Properties.Resources.Football_24,
                Properties.Resources.Football_25,
                Properties.Resources.Football_26,
                Properties.Resources.Football_27,
                Properties.Resources.Football_28,
                Properties.Resources.Football_29
            };

        public static void DrawARTL(Graphics g, PSALroute poso, Point ARTXY)
        {
            float gSize = (g.DpiX * 5.33333f);
            float scale = gSize / 180f;
            int eol = poso.aRTL.ct.ToList().IndexOf(poso.aRTL.ct.ToList().Where(p => p != 0).LastOrDefault());

            g.SmoothingMode = SmoothingMode.AntiAlias;

            #region Define the Route and Zone Pen, Zone SolidBrush and Custom Blocking or Arrow End Cap

            //Define Route Pen

            Pen routePen = new Pen(poso.RouteColor.Color, 3f * scale);
            routePen.LineJoin = LineJoin.MiterClipped;
            routePen.Color = poso.RouteColor.Color;

            //Define the Zone Pen and Zone SolidBrush

            Pen zonePen = new Pen(poso.RouteColor.Color, 1.5f * scale);
            SolidBrush zoneBrush = new SolidBrush(Color.FromArgb(215, poso.RouteColor.Color));

            //Define the Blocking Line Endcap

            if (poso.aRTL.ct[eol] == 1)
            {
                GraphicsPath blockingPath = new GraphicsPath();
                blockingPath.AddLine(-1.75f, .4f, 1.75f, .4f);
                CustomLineCap blockingEndCap = new CustomLineCap(null, blockingPath);
                blockingEndCap.WidthScale = .35f * scale;
                routePen.CustomEndCap = blockingEndCap;
            }

            //Define the Arrow Endcap
            else if (poso.aRTL.ct[eol] == 2)
            {
                GraphicsPath customArrow = new GraphicsPath();
                PointF[] arrowPath = new PointF[] { new PointF(-.275f * scale, .4f), new PointF(.275f * scale, .4f), new PointF(0, .7f * scale), new PointF(-.275f * scale, .4f) };
                customArrow.AddPolygon(arrowPath);
                CustomLineCap customArrowCap = new CustomLineCap(null, customArrow);
                routePen.CustomEndCap = customArrowCap;
            }

            #endregion

            //Draw the zone ellipse and route

            g.FillEllipse(zoneBrush, poso.zoneXY.X - ((poso.zoneSize.X * scale) / 2), poso.zoneXY.Y - ((poso.zoneSize.Y * scale) / 2), poso.zoneSize.X * scale, poso.zoneSize.Y * scale);
            g.DrawEllipse(zonePen, poso.zoneXY.X - ((poso.zoneSize.X * scale) / 2), poso.zoneXY.Y - ((poso.zoneSize.Y * scale) / 2), poso.zoneSize.X * scale, poso.zoneSize.Y * scale);
            g.DrawPath(routePen, poso.ARTLroute);
            if (!(poso.ARTLrouteOption1 == null))
            {
                routePen.Color = Color.FromArgb(128, poso.RouteColor.Color);
                g.DrawPath(routePen, poso.ARTLrouteOption1);
            }
            if (!(poso.ARTLrouteOption2 == null))
            {
                routePen.Color = Color.FromArgb(128, poso.RouteColor.Color);
                g.DrawPath(routePen, poso.ARTLrouteOption2);
            }
        }

        public static PSALroute ConvertARTL(Graphics g, PSALroute poso, Point ARTXY)
        {
            float gSize = (g.DpiX * 5.33333f);
            float scale = gSize / 180f;
            int EndOfList = poso.aRTL.ct.ToList().FindIndex(p => p != 0);
            if (EndOfList < 0) EndOfList = 0;
            int EndOfOption = poso.aRTL.ct.ToList().FindLastIndex(p => p != 0);
            bool IsOption = false;
            int NumberOFOptions = EndOfOption - EndOfList;

            if (EndOfOption > EndOfList) IsOption = true;

            if (EndOfList >= 0) if (poso.aRTL.ct[EndOfList] == 1)
                {
                    poso.RouteColor = PSALColor.Block;
                    if (poso.Position == "T" || poso.Position == "G" || poso.Position == "C" || poso.Position.Contains("TE")) EndOfList = EndOfOption;
                    IsOption = false;
                }

            //Get Zone XY and ZoneSize
            if (poso.CPFMtype == "Defense")
            {
                //Get Zone XY
                if (poso.aRTL.au[EndOfList] == 0 && poso.aRTL.av[EndOfList] == 0)
                {
                    poso.zoneXY = new PointF(
                        ((poso.aRTL.ax[EndOfList] + ARTXY.X) * scale * .9f) + (gSize * .05f),
                        ((poso.aRTL.ay[EndOfList] + ARTXY.Y) * scale * .95f) + (gSize * .05f)
                        );
                }
                else
                {
                    poso.zoneXY = new PointF(
                        ((poso.aRTL.au[EndOfList] + ARTXY.X) * scale * .9f) + (gSize * .05f),
                        ((poso.aRTL.av[EndOfList] + ARTXY.Y) * scale * .95f) + (gSize * .05f)
                        );
                }

                //Get Zone Size
                switch (poso.aRTL.ct[EndOfList])
                {
                    case 3: //Deep quarter, hook, flat
                        poso.zoneSize = ARTL.QuarterHookFlat;
                        break;

                    case 4: //Deep third
                        poso.zoneSize = ARTL.DeepThird;
                        break;

                    case 5: //Deep half
                        poso.zoneSize = ARTL.DeepHalf;
                        break;

                    case 6: //QB spy
                        poso.zoneSize = ARTL.QBSpy;
                        break;
                }
            }

            //Get ARTL GraphicsPath
            GraphicsPath route = new GraphicsPath();
            GraphicsPath routeOption1 = new GraphicsPath();
            GraphicsPath routeOption2 = new GraphicsPath();
            PointF[] curve = new PointF[4];
            PointF[] line = new PointF[2];

            //Get ARTL Route
            for (int i = 0; i <= EndOfList; i++)
            {
                if (poso.aRTL.au[i] == 0 && poso.aRTL.av[i] == 0)
                {
                    if (i == 0)
                    {
                        line[0] = new PointF((ARTXY.X * scale * .9f) + (gSize * .05f), (ARTXY.Y * scale * .95f) + (gSize * .05f));
                        line[1] = new PointF(((poso.aRTL.ax[i] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[i] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        route.AddLine(line[0], line[1]);
                    }
                    else
                    {
                        if (!(poso.aRTL.au[i - 1] == 0) && !(poso.aRTL.av[i - 1] == 0))
                        {
                            line[0] = new PointF(((poso.aRTL.au[i - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[i - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            line[1] = new PointF(((poso.aRTL.ax[i] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[i] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            route.AddLine(line[0], line[1]);
                        }
                        else
                        {
                            line[0] = new PointF(((poso.aRTL.ax[i - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[i - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            line[1] = new PointF(((poso.aRTL.ax[i] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[i] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            route.AddLine(line[0], line[1]);
                        }
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        curve[0] = new PointF((ARTXY.X * scale * .9f) + (gSize * .05f), (ARTXY.Y * scale * .95f) + (gSize * .05f));
                        curve[1] = new PointF((ARTXY.X * scale * .9f) + (gSize * .05f), (ARTXY.Y * scale * .95f) + (gSize * .05f));
                        curve[2] = new PointF(((poso.aRTL.ax[i] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[i] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        curve[3] = new PointF(((poso.aRTL.au[i] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[i] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        route.AddBeziers(curve);
                    }
                    else
                    {
                        if (!(poso.aRTL.au[i - 1] == 0) && !(poso.aRTL.av[i - 1] == 0))
                        {
                            curve[0] = new PointF(((poso.aRTL.au[i - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[i - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[1] = new PointF(((poso.aRTL.au[i - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[i - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[2] = new PointF(((poso.aRTL.ax[i] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[i] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[3] = new PointF(((poso.aRTL.au[i] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[i] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            route.AddBeziers(curve);
                        }
                        else
                        {
                            curve[0] = new PointF(((poso.aRTL.ax[i - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[i - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[1] = new PointF(((poso.aRTL.ax[i - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[i - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[2] = new PointF(((poso.aRTL.ax[i] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[i] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[3] = new PointF(((poso.aRTL.au[i] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[i] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            route.AddBeziers(curve);
                        }
                    }
                }
            }

            poso.ARTLroute = route;

            //Get ARTL Option Route(s)
            if (IsOption)
            {
                if (poso.aRTL.au[EndOfList + 1] == 0 && poso.aRTL.av[EndOfList + 1] == 0)
                {
                    if (!(poso.aRTL.au[EndOfList] == 0) && !(poso.aRTL.av[EndOfList] == 0))
                    {
                        line[0] = new PointF(((poso.aRTL.au[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        line[1] = new PointF(((poso.aRTL.ax[EndOfList + 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList + 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        routeOption1.AddLine(line[0], line[1]);
                    }
                    else
                    {
                        line[0] = new PointF(((poso.aRTL.ax[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        line[1] = new PointF(((poso.aRTL.ax[EndOfList + 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList + 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        routeOption1.AddLine(line[0], line[1]);
                    }
                }
                else
                {
                    if (!(poso.aRTL.au[EndOfList] == 0) && !(poso.aRTL.av[EndOfList] == 0))
                    {
                        curve[0] = new PointF(((poso.aRTL.au[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        curve[1] = new PointF(((poso.aRTL.au[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        curve[2] = new PointF(((poso.aRTL.ax[EndOfList + 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList + 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        curve[3] = new PointF(((poso.aRTL.au[EndOfList + 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList + 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        routeOption1.AddBeziers(curve);
                    }
                    else
                    {
                        curve[0] = new PointF(((poso.aRTL.ax[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        curve[1] = new PointF(((poso.aRTL.ax[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        curve[2] = new PointF(((poso.aRTL.ax[EndOfList + 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList + 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        curve[3] = new PointF(((poso.aRTL.au[EndOfList + 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList + 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                        routeOption1.AddBeziers(curve);
                    }
                }

                poso.ARTLrouteOption1 = routeOption1;

                if (NumberOFOptions == 2)
                {
                    if (poso.aRTL.au[EndOfList + 2] == 0 && poso.aRTL.av[EndOfList + 2] == 0)
                    {
                        if (!(poso.aRTL.au[EndOfList + 1] == 0) && !(poso.aRTL.av[EndOfList + 1] == 0))
                        {
                            line[0] = new PointF(((poso.aRTL.au[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            line[1] = new PointF(((poso.aRTL.ax[EndOfList + 2] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList + 2] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            routeOption2.AddLine(line[0], line[1]);
                        }
                        else
                        {
                            line[0] = new PointF(((poso.aRTL.ax[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            line[1] = new PointF(((poso.aRTL.ax[EndOfList + 2] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList + 2] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            routeOption2.AddLine(line[0], line[1]);
                        }
                    }
                    else
                    {
                        if (!(poso.aRTL.au[EndOfList] == 0) && !(poso.aRTL.av[EndOfList] == 0))
                        {
                            curve[0] = new PointF(((poso.aRTL.au[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[1] = new PointF(((poso.aRTL.au[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[2] = new PointF(((poso.aRTL.ax[EndOfList + 2] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList + 2] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[3] = new PointF(((poso.aRTL.au[EndOfList + 2] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList + 2] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            routeOption2.AddBeziers(curve);
                        }
                        else
                        {
                            curve[0] = new PointF(((poso.aRTL.ax[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[1] = new PointF(((poso.aRTL.ax[EndOfList - 1] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList - 1] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[2] = new PointF(((poso.aRTL.ax[EndOfList + 2] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.ay[EndOfList + 2] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            curve[3] = new PointF(((poso.aRTL.au[EndOfList + 2] + ARTXY.X) * scale * .9f) + (gSize * .05f), ((poso.aRTL.av[EndOfList + 2] + ARTXY.Y) * scale * .95f) + (gSize * .05f));
                            routeOption2.AddBeziers(curve);
                        }
                    }

                    poso.ARTLrouteOption2 = routeOption2;

                }
            }

            Console.WriteLine
            (
                poso.Position + "\n" +
                "XY: " + poso.playerXY + "\n" +
                "EndOfList: " + EndOfList + "\n" +
                "Option Route: " + IsOption + "(" + NumberOFOptions + ")" + "\n" +
                "Route Points: " + route.PointCount + "\n" +
                "OptionRoute1 Points: " + routeOption1.PointCount + "\n" +
                "OptionRoute2 Points: " + routeOption2.PointCount + "\n" +
                poso.aRTL.ToString()
            );

            return poso;
        }

        public static void DrawARTLHighlight(Graphics g, float playerSize, float X, float Y)
        {
            float gSize = (g.DpiX * 5.33333f);
            float scale = gSize / 180f;

            //define highlighted player gradient ellipse
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(
                ((X * scale * .9f) + (gSize * .05f)) - ((playerSize * scale) / 2),
                ((Y * scale * .95f) + (gSize * .05f)) - ((playerSize * scale) / 2), 
                (playerSize * scale), 
                (playerSize * scale)
                );
            PathGradientBrush pthGrBrush = new PathGradientBrush(path);
            pthGrBrush.CenterColor = PSALColor.PlayerHighlightColor;
            Color[] colors = { Color.Transparent };
            pthGrBrush.SurroundColors = colors;

            g.FillEllipse(
                pthGrBrush, 
                ((X * scale * .9f) + (gSize * .05f)) - ((playerSize * scale) / 2),
                ((Y * scale * .95f) + (gSize * .05f)) - ((playerSize * scale) / 2),
                (playerSize * scale),
                (playerSize * scale)
                );

            path.Dispose();
            pthGrBrush.Dispose();
        }

        public static void DrawARTLIcon(Graphics g, PSALroute poso, Point ARTXY)
        {
            float gSize = (g.DpiX * 5.33333f);
            float scale = gSize / 180f;

            SolidBrush sb = new SolidBrush(PSALColor.PlayerIconColor);

            if (poso.Position.Contains("RE") || poso.Position.Contains("DT") || poso.Position.Contains("LE") || poso.Position == "C")
            {
                g.FillRectangle(sb, (((ARTXY.X* scale * .9f) + (gSize* .05f)) - (7f * scale) / 2), (((ARTXY.Y* scale * .95f) + (gSize* .05f)) - (7f * scale) / 2), (7f * scale), (7f * scale));
            }
            else if (poso.Position.Contains("CB") || poso.Position.Contains("SS") || poso.Position.Contains("FS"))
            {
                GraphicsPath gpX = new GraphicsPath();
                Pen Xpen = new Pen(PSALColor.PlayerIconColor, 1.5f * scale);
                gpX.AddString(
                    "X",
                    FontFamily.GenericSansSerif,
                    (int) FontStyle.Regular,
                    g.DpiY* (5.5f * scale) / 72,
                    new PointF((ARTXY.X* scale * .9f) + (gSize* .05f), (ARTXY.Y* scale * .95f) + (gSize* .05f)),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                g.DrawPath(Xpen, gpX);
            }
            else
            {
                g.FillEllipse(sb, (((ARTXY.X* scale * .9f) + (gSize* .05f)) - (7f * scale) / 2), (((ARTXY.Y* scale * .95f) + (gSize* .05f)) - (7f * scale) / 2), (7f * scale), (7f * scale));
            }
        }

        public static void DrawPSAL(Graphics g, PSALroute PSAL)
        {
            //define route line
            Pen pen = new Pen(PSAL.RouteColor.Color, 5);
            pen.LineJoin = LineJoin.Round;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //define blocking endcap
            GraphicsPath blockingPath = new GraphicsPath();
            blockingPath.AddLine(-2.1f, 0, 2.1f, 0);
            CustomLineCap blockingEndCap = new CustomLineCap(null, blockingPath);
            blockingEndCap.WidthScale = .8f;

            //Define the Zone Pen, Zone SolidBrush and Zone End Caps

            Pen zonePen = new Pen(PSAL.RouteColor.Color, 3);
            SolidBrush zoneBrush = new SolidBrush(Color.FromArgb(215, PSAL.RouteColor.Color));

            if (PSAL.CPFMtype == "Defense")
            {
                g.FillEllipse(zoneBrush, PSAL.route[PSAL.route.Count - 1].X - (PSAL.zoneSize.X), PSAL.route[PSAL.route.Count - 1].Y - (PSAL.zoneSize.X), PSAL.zoneSize.X * 2, PSAL.zoneSize.X * 2);
                g.DrawEllipse(zonePen, PSAL.route[PSAL.route.Count - 1].X - (PSAL.zoneSize.X), PSAL.route[PSAL.route.Count - 1].Y - (PSAL.zoneSize.X), PSAL.zoneSize.X * 2, PSAL.zoneSize.X * 2);
            }

            // create AdjustableArrowCap or no end cap if blocking

            switch (PSAL.RouteColor.Type)
            {
                case "Block":
                    pen.CustomEndCap = blockingEndCap;
                    break;
                case "Base Route":
                    pen.CustomEndCap = new AdjustableArrowCap(3, 4);
                    break;
                case "Primary Route":
                    pen.CustomEndCap = new AdjustableArrowCap(3, 4);
                    break;
                case "Delay Route":
                    pen.CustomEndCap = new AdjustableArrowCap(3, 4);
                    break;
                case "Motion Route":
                    pen.CustomEndCap = new AdjustableArrowCap(3, 4);
                    break;
                case "Run":
                    pen.CustomEndCap = new AdjustableArrowCap(3, 4);
                    break;
                case "QB Scramble":
                    pen.CustomEndCap = new AdjustableArrowCap(3, 4);
                    break;
                case "QB Handoff":
                    pen.CustomEndCap = new AdjustableArrowCap(3, 4);
                    break;
                case "Kickoff":
                    pen.CustomEndCap = new AdjustableArrowCap(3, 4);
                    break;
                case "Rush QB":
                    pen.CustomEndCap = new AdjustableArrowCap(3, 4);
                    break;
                default:
                    break;
            }

            //draw PSAL
            try
            {
                if (PSAL.route.Count > 1)
                {
                    g.DrawLines(pen, XYtoPoint(PSAL.route));
                }

                //draw option route 1
                if (!(PSAL.routeOption1 == null))
                {
                    pen.Color = Color.FromArgb(128, pen.Color);
                    g.DrawLines(pen, XYtoPoint(PSAL.routeOption1));
                }

                //draw option route 2
                if (!(PSAL.routeOption2 == null))
                {
                    pen.Color = Color.FromArgb(128, pen.Color);
                    g.DrawLines(pen, XYtoPoint(PSAL.routeOption2));
                }
            }
            catch{}

            pen.Dispose();
        }

        public static PSALroute ConvertPSAL(PSALroute PSAL, Point LOS, bool flipPSAL)
        {
            //translate the PSAL vals to XY in pixels based on the player position and define the route color
            PSAL.route = new List<XY>();
            PSAL.route.Add(PSAL.playerXY);

            for (int i = 0; i < PSAL.steps.Count; i++)
            {
                switch (PSAL.steps[i].code)
                {
                    case 1:
                        #region Run To End Zone

                        break;

                    #endregion

                    case 2:
                        #region Chase Ball

                        break;

                    #endregion

                    case 3:
                        #region MoveDirDist

                        //convert to offest
                        PSAL.route.Add(MoveDistDirToXY(PSAL.steps[i].val1, PSAL.steps[i].val2, PSAL.AngleRatio));
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        };

                        //assign route color
                        if (PSAL.RouteColor.Equals(PSALColor.Undefined))
                        {
                            PSAL.RouteColor = PSALColor.BaseRoute;
                        }
                        break;

                    #endregion

                    case 4:
                        #region MoveDirDistConst

                        //convert to offest
                        PSAL.route.Add(MoveDistDirToXY(PSAL.steps[i].val1, PSAL.steps[i].val2, PSAL.AngleRatio));
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        };

                        //assign route color
                        if (PSAL.RouteColor.Equals(PSALColor.Undefined))
                        {
                            PSAL.RouteColor = PSALColor.BaseRoute;
                        }
                        break;

                    #endregion

                    case 5:
                        #region Face Direction

                        break;

                    #endregion

                    case 7:
                        #region QB Scramble

                        //assign route color
                        PSAL.RouteColor = PSALColor.QBScramble;
                        break;

                    #endregion

                    case 8:
                        #region Receiver Run Route

                        //convert to offest
                        PSAL.route.Add(MoveDistDirToXY(PSAL.steps[i].val1, PSAL.steps[i].val2, PSAL.AngleRatio));
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        };

                        if (PSAL.route[PSAL.route.Count - 1].X < 50 || PSAL.route[PSAL.route.Count - 1].X > 483)
                        {
                            PSAL.route[PSAL.route.Count - 1] = (MoveDistDirToXY(60, PSAL.steps[i].val2, PSAL.AngleRatio));
                            if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //assign route color
                        if (PSAL.RouteColor.Equals(PSALColor.Undefined))
                        {
                            PSAL.RouteColor = PSALColor.BaseRoute;
                        }
                        break;

                    #endregion

                    case 9:
                        #region Receiver Cut Move
                        //1 = 45 degrees
                        if (PSAL.steps[i].val2 == 1)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 - (45 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 + (45 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //2 = 90 degrees
                        if (PSAL.steps[i].val2 == 2)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 - (90 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 + (90 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //3 = 22 degrees
                        if (PSAL.steps[i].val2 == 3)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 - (22 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 + (22 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //4 = 67 degrees
                        if (PSAL.steps[i].val2 == 4)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 - (67 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 + (67 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //5 = Curl
                        if (PSAL.steps[i].val2 == 5)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 - (135 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 + (135 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //7 = HitchComback, 8 = HitchGoIn and 9 = HitchGoOut
                        if (PSAL.steps[i].val2 == 7 || PSAL.steps[i].val2 == 8 || PSAL.steps[i].val2 == 9)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(8, (int)(PSAL.steps[i - 1].val2 - (105 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(8, (int)(PSAL.steps[i - 1].val2 + (105 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //10 = OutAndUp
                        if (PSAL.steps[i].val2 == 10)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 - (105 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(PSAL.steps[i - 1].val2 + (105 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //11 = Smash and 17 = SmashQuick
                        if (PSAL.steps[i].val2 == 11 || PSAL.steps[i].val2 == 17)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(32, (int)(-10 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(32, (int)(190 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //13 = WRScrn
                        if (PSAL.steps[i].val2 == 13)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(-15 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(195 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };

                            //assign route color
                            if (PSAL.RouteColor.Equals(PSALColor.Undefined))
                            {
                                PSAL.RouteColor = PSALColor.BaseRoute;
                            }
                        }

                        //14 = 90Inside
                        if (PSAL.steps[i].val2 == 14)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(0 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(180 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //16 = 180Partial
                        if (PSAL.steps[i].val2 == 16)
                        {
                            PSAL.route.Add(MoveDistDirToXY(24, (int)(270 * PSAL.AngleRatio), PSAL.AngleRatio));
                            if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //18 = Hitch
                        if (PSAL.steps[i].val2 == 18)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(32, (int)(PSAL.steps[i - 1].val2 - (105 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(32, (int)(PSAL.steps[i - 1].val2 + (105 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //20 = ShakeCut
                        if (PSAL.steps[i].val2 == 20)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(8, (int)(PSAL.steps[i - 1].val2 + (67 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(8, (int)(PSAL.steps[i - 1].val2 - (67 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //21 = StutterCut
                        if (PSAL.steps[i].val2 == 21)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(8, (int)(PSAL.steps[i - 1].val2 - (67 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(8, (int)(PSAL.steps[i - 1].val2 + (67 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //22 = HingeCut
                        if (PSAL.steps[i].val2 == 22)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(32, (int)(145 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(32, (int)(35 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //23 = PostCorner
                        if (PSAL.steps[i].val2 == 23)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(48, (int)(PSAL.steps[i - 1].val2 + (45 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(48, (int)(PSAL.steps[i - 1].val2 - (45 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //25 = StutterStreak
                        if (PSAL.steps[i].val2 == 25)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(4, (int)(PSAL.steps[i - 1].val2 + (90 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(4, (int)(PSAL.steps[i - 1].val2 - (90 * PSAL.AngleRatio)), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //26 = WR Swing
                        if (PSAL.steps[i].val2 == 26)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(-35 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                                PSAL.route[PSAL.route.Count - 1] = new XY
                                {
                                    X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                    Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                                };
                                //PSAL.steps[i].val2 = (int)(-35 * PSAL.AngleRatio);
                                i++;
                                PSAL.steps.Insert(i, PSAL.steps[i - 1].DeepCopy());

                                PSAL.route.Add(MoveDistDirToXY(24, (int)(0 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                                PSAL.route[PSAL.route.Count - 1] = new XY
                                {
                                    X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                    Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                                };
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(215 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                                PSAL.route[PSAL.route.Count - 1] = new XY
                                {
                                    X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                    Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                                };
                                //PSAL.steps[i].val2 = (int)(215 * PSAL.AngleRatio);
                                i++;
                                PSAL.steps.Insert(i, PSAL.steps[i - 1].DeepCopy());

                                PSAL.route.Add(MoveDistDirToXY(24, (int)(180 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                                PSAL.route[PSAL.route.Count - 1] = new XY
                                {
                                    X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                    Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                                };
                            }
                        }

                        //28 = Sluggo
                        if (PSAL.steps[i].val2 == 28)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(22 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(158 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //29 = Out n Up
                        if (PSAL.steps[i].val2 == 29)
                        {
                            if (PSAL.steps[i].val1 == 1)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(180 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            else if (PSAL.steps[i].val1 == 2)
                            {
                                PSAL.route.Add(MoveDistDirToXY(24, (int)(0 * PSAL.AngleRatio), PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            }
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        break;
                    #endregion

                    case 10:
                        #region Receiver Get Open

                        break;

                    #endregion

                    case 11:
                        #region Pitch Ball?

                        //assign route color
                        PSAL.RouteColor = PSALColor.QBHandoff;
                        break;

                    #endregion

                    case 12:
                        #region Option Handoff

                        //assign route color
                        PSAL.RouteColor = PSALColor.QBHandoff;
                        break;

                    #endregion

                    case 13:
                        #region Receive Hand Off

                        //convert to offest
                        PSAL.route.Add(MoveDistDirToXY(PSAL.steps[i].val1, PSAL.steps[i].val2, PSAL.AngleRatio));
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        };

                        //assign route color
                        PSAL.RouteColor = PSALColor.Run;
                        break;

                    #endregion

                    case 14:
                        #region PassBlock

                        if (PSAL.steps[i].val1 == 0)
                        {
                            //assign route color
                            PSAL.RouteColor = PSALColor.Block;

                            //manual offset of 2 yards back
                            if (i == 0)
                            {
                                PSAL.route.Add(MoveDistDirToXY(16, 96, PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                                //convert to offest
                                PSAL.route[PSAL.route.Count - 1] = new XY
                                {
                                    X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                    Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                                };
                            }
                        }
                        else
                        {
                            PSAL.route.Add(PSAL.route[PSAL.route.Count - 1]);

                            //assign route color
                            PSAL.RouteColor = PSALColor.DelayRoute;
                        }

                        break;

                    #endregion

                    case 15:
                        #region RunBlock

                        if (PSAL.steps[i].val1 == 0)
                        {
                            //assign route color
                            PSAL.RouteColor = PSALColor.Block;

                            //manual offset of 2 yards forward
                            if (i == 0)
                            {
                                PSAL.route.Add(MoveDistDirToXY(16, 32, PSAL.AngleRatio));
                                if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                                //convert to offest
                                PSAL.route[PSAL.route.Count - 1] = new XY
                                {
                                    X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                    Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                                };
                            }
                        }
                        else
                        {
                            PSAL.route.Add(PSAL.route[PSAL.route.Count - 1]);

                            //assign route color
                            PSAL.RouteColor = PSALColor.DelayRoute;
                        }

                        break;

                    #endregion

                    case 16:
                        #region Kickoff?

                        //assign route color
                        PSAL.RouteColor = PSALColor.Kickoff;
                        break;

                    #endregion

                    case 18:
                        #region LeadBlock

                        ////convert to offest
                        //PSAL.route.Add(MoveDirDistToXY(PSAL.steps[i].val1, PSAL.steps[i].val2, PSAL.AngleRatio));
                        //PSAL.route[PSAL.route.Count - 1] = new XY
                        //{
                        //    X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                        //    Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        //};

                        //assign route color
                        PSAL.RouteColor = PSALColor.Block;
                        break;

                    #endregion

                    case 19:
                        #region Man Coverage

                        break;

                    #endregion

                    case 20:
                        #region Cloud flat, Hard Flat, Soft Squat 

                        if (PSAL.steps[i].val1 == 9) PSAL.route.Add(new XY { X = (int)(533 * 1 / 6), Y = 400 });
                        else if (PSAL.steps[i].val1 == 10) PSAL.route.Add(new XY { X = (int)(533 * 5 / 6), Y = 400 });

                        switch (PSAL.steps[i].val2)
                        {
                            case 0:
                                PSAL.RouteColor = PSALColor.CloudFlat;
                                break;
                            case 1:
                                PSAL.RouteColor = PSALColor.HardFlat;
                                break;
                            case 2:
                                PSAL.RouteColor = PSALColor.SoftSquat;
                                break;
                            default:
                                PSAL.RouteColor = PSALColor.CloudFlat;
                                break;
                        }
                        break;

                    #endregion

                    case 21:
                        #region Mid Read, 3 Rec Hook, Hook Curl, Vert Hook  

                        //convert to offest
                        PSAL.route.Add(MoveDistDirToXY(20, PSAL.steps[i].val2, PSAL.AngleRatio));
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        };

                        switch (PSAL.steps[i].val1)
                        {
                            case 0:
                                PSAL.route.Add(new XY { X = (int)(533 / 2), Y = 250 });
                                PSAL.RouteColor = PSALColor.MidRead;
                                break;
                            case 1:
                                PSAL.route.Add(new XY { X = (int)(533 / 2), Y = 300 });
                                PSAL.RouteColor = PSALColor.ThreeReceiverHook;
                                break;
                            case 2:
                                PSAL.route.Add(new XY { X = (int)(533 * 2 / 6), Y = 350 });
                                PSAL.RouteColor = PSALColor.HookCurl;
                                break;
                            case 3:
                                PSAL.route.Add(new XY { X = (int)(533 * 4 / 6), Y = 350 });
                                PSAL.RouteColor = PSALColor.HookCurl;
                                break;
                            case 4:
                                PSAL.route.Add(new XY { X = (int)(533 * 2 / 6), Y = 350 });
                                PSAL.RouteColor = PSALColor.VertHook;
                                break;
                            case 5:
                                PSAL.route.Add(new XY { X = (int)(533 * 4 / 6), Y = 350 });
                                PSAL.RouteColor = PSALColor.VertHook;
                                break;
                            default:
                                PSAL.RouteColor = PSALColor.HookCurl;
                                break;
                        }
                        break;

                    #endregion

                    case 22:
                        #region Curl Flat, Seam Flat, Quarter Flat  

                        if (PSAL.steps[i].val1 == 11) PSAL.route.Add(new XY { X = (int)(533 * 1 / 6), Y = 350 });
                        else if (PSAL.steps[i].val1 == 12) PSAL.route.Add(new XY { X = (int)(533 * 5 / 6), Y = 350 });

                        switch (PSAL.steps[i].val2)
                        {
                            case 0:
                                PSAL.RouteColor = PSALColor.CurlFlat;
                                break;
                            case 1:
                                PSAL.RouteColor = PSALColor.SeamFlat;
                                break;
                            case 2:
                                PSAL.RouteColor = PSALColor.QuarterFlat;
                                break;
                            default:
                                PSAL.RouteColor = PSALColor.SeamFlat;
                                break;
                        }
                        break;

                    #endregion

                    case 23:
                        #region Deep Zone  

                        switch (PSAL.steps[i].val1)
                        {
                            case 0:
                                PSAL.route.Add(new XY { X = (int)(533 * 1 / 4), Y = 150});
                                break;
                            case 1:
                                PSAL.route.Add(new XY { X = (int)(533 * 3 / 4), Y = 150 });
                                break;
                            case 2:
                                PSAL.route.Add(new XY { X = (int)(533 * 1 / 6), Y = 150 });
                                break;
                            case 3:
                                PSAL.route.Add(new XY { X = (int)(533 * 3 / 6), Y = 150 });
                                break;
                            case 4:
                                PSAL.route.Add(new XY { X = (int)(533 * 5 / 6), Y = 150 });
                                break;
                            case 5:
                                PSAL.route.Add(new XY { X = (int)(533 * 1 / 8), Y = 150 });
                                break;
                            case 6:
                                PSAL.route.Add(new XY { X = (int)(533 * 3 / 8), Y = 150 });
                                break;
                            case 7:
                                PSAL.route.Add(new XY { X = (int)(533 * 5 / 8), Y = 150 });
                                break;
                            case 8:
                                PSAL.route.Add(new XY { X = (int)(533 * 7 / 8), Y = 150 });
                                break;
                            default:
                                ;
                                break;
                        }

                        PSAL.RouteColor = PSALColor.DeepZone;

                        break;

                    #endregion

                    case 24:
                        #region Pass Rush  

                        //convert to offest
                        PSAL.route.Add(MoveDistDirToXY(PSAL.steps[i].val2, PSAL.steps[i].val1, PSAL.AngleRatio));
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        };

                        PSAL.RouteColor = PSALColor.RushQB;

                        break;

                    #endregion

                    case 25:
                        #region Delay

                        break;

                    #endregion

                    case 26:
                        #region Initial Anim

                        if (PSAL.steps[i].val1 == 1 || PSAL.steps[i].val1 == 4 || PSAL.steps[i].val1 == 5 || PSAL.steps[i].val1 == 6)
                        {
                            //convert to offest
                            PSAL.route.Add(MoveDistDirToXY(4, PSAL.steps[i].val2, PSAL.AngleRatio));
                            if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }
                        else if (PSAL.steps[i].val1 == 25)
                        {
                            //convert to offest
                            PSAL.route.Add(MoveDistDirToXY(32, PSAL.steps[i].val2, PSAL.AngleRatio));
                            if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }
                        else if (PSAL.steps[i].val1 == 15)
                        {
                            //convert to offest
                            PSAL.route.Add(MoveDistDirToXY(PSAL.steps[i].val2, 96, PSAL.AngleRatio));
                            if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                            PSAL.route[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                                Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                            };
                        }

                        //assign route color
                        if (PSAL.RouteColor.Equals(PSALColor.Undefined))
                        {
                            PSAL.RouteColor = PSALColor.BaseRoute;
                        }
                        break;

                    #endregion

                    case 27:
                        #region Punt?

                        break;

                    #endregion

                    case 28:
                        #region FG Spot?

                        break;

                    #endregion

                    case 29:
                        #region FG Kick?

                        break;

                    #endregion

                    case 30:
                        #region Stop Clock?

                        break;

                    #endregion

                    case 31:
                        #region Kneel?

                        break;

                    #endregion

                    case 32:
                        #region Receive Pitch

                        //convert to offest
                        PSAL.route.Add(MoveDistDirToXY(PSAL.steps[i].val1, PSAL.steps[i].val2, PSAL.AngleRatio));
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        };

                        //assign route color
                        PSAL.RouteColor = PSALColor.Run;
                        break;

                    #endregion

                    case 34:
                        #region QB Spy

                        PSAL.RouteColor = PSALColor.QBSpy;

                        break;

                    #endregion

                    case 35:
                        #region Head Turn Run Route

                        //convert to offest
                        PSAL.route.Add(MoveDistDirToXY(PSAL.steps[i].val1, PSAL.steps[i].val2, PSAL.AngleRatio));
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        };
                        break;

                    #endregion

                    case 36:
                        #region Option Route

                        //Option Route Base
                        PSAL.route.Add(GetOptionOffset(PSAL.steps[i].val1, PSAL.AngleRatio));
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;

                        //Option route 1
                        PSAL.routeOption1 = new List<XY>(PSAL.route);
                        PSAL.routeOption1[PSAL.routeOption1.Count - 1] = GetOptionOffset(PSAL.steps[i].val2, PSAL.AngleRatio);
                        if (flipPSAL) PSAL.routeOption1[PSAL.routeOption1.Count - 1].X = PSAL.routeOption1[PSAL.routeOption1.Count - 1].X * -1;
                        PSAL.routeOption1[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.routeOption1[PSAL.routeOption1.Count - 2].X + PSAL.routeOption1[PSAL.routeOption1.Count - 1].X,
                            Y = PSAL.routeOption1[PSAL.routeOption1.Count - 2].Y + PSAL.routeOption1[PSAL.routeOption1.Count - 1].Y
                        };

                        //Option route 2
                        if (!(PSAL.steps[i].val3 == 255))
                        {
                            PSAL.routeOption2 = new List<XY>(PSAL.route);
                            PSAL.routeOption2[PSAL.routeOption2.Count - 1] = GetOptionOffset(PSAL.steps[i].val3, PSAL.AngleRatio);
                            if (flipPSAL) PSAL.routeOption2[PSAL.routeOption2.Count - 1].X = PSAL.routeOption2[PSAL.routeOption2.Count - 1].X * -1;
                            PSAL.routeOption2[PSAL.route.Count - 1] = new XY
                            {
                                X = PSAL.routeOption2[PSAL.routeOption2.Count - 2].X + PSAL.routeOption2[PSAL.routeOption2.Count - 1].X,
                                Y = PSAL.routeOption2[PSAL.routeOption2.Count - 2].Y + PSAL.routeOption2[PSAL.routeOption2.Count - 1].Y
                            };
                        }

                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        };

                        break;

                    #endregion

                    case 37:
                        #region Option Route Extra Info

                        break;

                    #endregion

                    case 38:
                        #region Handoff Turn?

                        //assign route color
                        PSAL.RouteColor = PSALColor.QBHandoff;
                        break;

                    #endregion

                    case 39:
                        #region Handoff Give?

                        //assign route color
                        PSAL.RouteColor = PSALColor.QBHandoff;
                        break;

                    #endregion

                    case 40:
                        #region Option Run?

                        //assign route color
                        PSAL.RouteColor = PSALColor.Run;
                        break;

                    #endregion

                    case 41:
                        #region Rush QB

                        //convert to offest
                        //PSAL.route.Add(MoveDirDistToXY(PSAL.steps[i].val1, PSAL.steps[i].val2, PSAL.AngleRatio));
                        //if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        //PSAL.route[PSAL.route.Count - 1] = new XY
                        //{
                        //    X = PSAL.route[PSAL.route.Count - 2].X + PSAL.route[PSAL.route.Count - 1].X,
                        //    Y = PSAL.route[PSAL.route.Count - 2].Y + PSAL.route[PSAL.route.Count - 1].Y
                        //};

                        switch (PSAL.SRFT.GAPS)
                        {
                            case 1:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) + (16.5 * .5)), Y = 465 });
                                break;
                            case 2:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) - (16.5 * .5)), Y = 465 });
                                break;
                            case 3:
                                PSAL.route.Add(new XY { X = (int)(533 / 2), Y = 465 });
                                break;
                            case 4:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) + (16.5 * 1.5)), Y = 465 });
                                break;
                            case 5:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) + 16.5), Y = 465 });
                                break;
                            case 8:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) - (16.5 * 1.5)), Y = 465 });
                                break;
                            case 10:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) - 16.5), Y = 465 });
                                break;
                            case 16:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) + (16.5 * 2.5)), Y = 465 });
                                break;
                            case 20:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) + (16.5 * 2)), Y = 465 });
                                break;
                            case 32:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) - (16.5 * 2.5)), Y = 465 });
                                break;
                            case 40:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) - (16.5 * 2)), Y = 465 });
                                break;
                            case 64:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) + (16.5 * 3.5)), Y = 465 });
                                break;
                            case 128:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) - (16.5 * 3.5)), Y = 465 });
                                break;
                            case 256:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) + (16.5 * 4.5)), Y = 465 });
                                break;
                            case 512:
                                PSAL.route.Add(new XY { X = (int)((533 / 2) - (16.5 * 4.5)), Y = 465 });
                                break;
                            default:
                                break;
                        }

                        PSAL.RouteColor = PSALColor.RushQB;

                        break;

                    #endregion

                    case 42:
                        #region Hand Off Fake?

                        //assign route color
                        PSAL.RouteColor = PSALColor.QBScramble;
                        break;

                    #endregion

                    case 43:
                        #region Option Follow

                        //assign route color
                        PSAL.RouteColor = PSALColor.Run;
                        break;

                    #endregion

                    case 44:
                        #region Wedge Block

                        PSAL.RouteColor = PSALColor.Block;

                        break;

                    #endregion

                    case 45:
                        #region Auto Motion

                        //convert to offest
                        PSAL.route.Add(new XY { X = (int)(PSAL.steps[i].val1 / 5.6667 * 10), Y = (int)(Math.Abs(PSAL.steps[i].val2 / 5.6667 * 10)) });
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = LOS.X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = LOS.Y + PSAL.route[PSAL.route.Count - 1].Y
                        };

                        //assign route color
                        PSAL.RouteColor = PSALColor.MotionRoute;
                        break;

                    #endregion

                    case 46:
                        #region Auto Motion Snap

                        break;

                    #endregion

                    case 47:
                        #region Auto Motion Defense

                        //convert to offest
                        PSAL.route.Add(new XY { X = (int)(PSAL.steps[i].val1 / 5.6667 * 10), Y = (int)(Math.Abs(PSAL.steps[i].val2 / 5.6667 * 10)) });
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;
                        PSAL.route[PSAL.route.Count - 1] = new XY
                        {
                            X = LOS.X + PSAL.route[PSAL.route.Count - 1].X,
                            Y = LOS.Y - PSAL.route[PSAL.route.Count - 1].Y
                        };

                        break;

                    #endregion

                    case 48:
                        #region Player offset

                        PSAL.playerXY.X = LOS.X + (int)(PSAL.steps[i].val1 / 5.6667 * 10);
                        if (PSAL.CPFMtype == "Offense") PSAL.playerXY.Y = LOS.Y + Math.Abs((int)(PSAL.steps[i].val2 / 5.6667 * 10));
                        else if (PSAL.CPFMtype == "Defense") PSAL.playerXY.Y = LOS.Y - (int)(PSAL.steps[i].val2 / 5.6667 * 10);
                        if (flipPSAL) PSAL.route[PSAL.route.Count - 1].X = PSAL.route[PSAL.route.Count - 1].X * -1;

                        PSAL.route[0] = new XY { X = PSAL.playerXY.X, Y = PSAL.playerXY.Y };

                        //PSAL.route.Add(new XY
                        //{
                        //    X = PSAL.route[PSAL.route.Count - 1].X + (int)(PSAL.steps[i].val1 / 5.6667),
                        //    Y = PSAL.route[PSAL.route.Count - 1].Y + (int)(PSAL.steps[i].val2 / -8.3)
                        //};

                        break;

                    #endregion

                    case 57:
                        #region Animation Defense

                        break;

                    #endregion

                    case 58:
                        #region Animation Offense

                        break;

                    #endregion

                    case 255:
                        #region End of Route

                        break;

                        #endregion
                }
            }

            return PSAL;
        }

        public static void DrawPSALHighlight(Graphics g, int playerSize, float X, float Y, float scale)
        {
            //define highlighted player gradient ellipse
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse((int)((X * scale) - (playerSize * 6 / 2)), (int)((Y * scale) - (playerSize * 6 / 2)), playerSize * 6, playerSize * 6);
            PathGradientBrush pthGrBrush = new PathGradientBrush(path);
            pthGrBrush.CenterColor = Color.FromArgb(255, 64, 0, 255);
            Color[] colors = { Color.FromArgb(0, 0, 0, 0) };
            pthGrBrush.SurroundColors = colors;

            g.FillEllipse(pthGrBrush, (int)((X * scale) - (playerSize * 6 / 2)), (int)((Y * scale) - (playerSize * 6 / 2)), playerSize * 6, playerSize * 6);

            path.Dispose();
            pthGrBrush.Dispose();
        }

        public static void DrawPSALIcon(Graphics g, Color color, PSALroute PSAL, int playerSize, Point LOS)
        {
            //define basic player circle
            SolidBrush sb = new SolidBrush(color);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //define text properties
            StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            try
            {
                if (PSAL.Position == "C")
                {
                    g.FillRectangle(sb, (int)(PSAL.playerXY.X - (playerSize / 2)), (int)(PSAL.playerXY.Y - (playerSize / 2)), playerSize, playerSize);
                }
                else
                {
                    g.FillEllipse(sb, (int)(PSAL.playerXY.X - (playerSize / 2)), (int)(PSAL.playerXY.Y - (playerSize / 2)), playerSize, playerSize);
                }

                if (PSAL.Position.Length == 1)
                {
                    if (PSAL.Position == "T")
                    {
                        g.DrawString(PSAL.Position, new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, (float)(PSAL.playerXY.X + .5), (float)(PSAL.playerXY.Y + 1.5), format);
                    }
                    else
                    {
                        g.DrawString(PSAL.Position, new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, (float)(PSAL.playerXY.X + .5), PSAL.playerXY.Y + 1, format);
                    }
                }
                else
                {
                    if (PSAL.Position.Contains("RE") || PSAL.Position.Contains("DT") || PSAL.Position.Contains("LE"))
                    {
                        g.DrawString(PSAL.Position, new Font("Tahoma", 12, FontStyle.Bold), Brushes.White, (int)(PSAL.playerXY.X - (((float)(LOS.X - PSAL.playerXY.X) / (float)LOS.X) * (playerSize * 4))), PSAL.playerXY.Y - (int)(playerSize * 1.15), format);
                    }
                    else if (PSAL.Position.Contains("LB"))
                    {
                        g.DrawString(PSAL.Position, new Font("Tahoma", 12, FontStyle.Bold), Brushes.White, (int)(PSAL.playerXY.X - (((float)(LOS.X - PSAL.playerXY.X) / (float)LOS.X) * (playerSize * 8))), PSAL.playerXY.Y - (int)(playerSize * 1.85), format);
                    }
                    else if (PSAL.Position.Contains("CB") || PSAL.Position.Contains("SS") || PSAL.Position.Contains("FS"))
                    {
                        g.DrawString(PSAL.Position, new Font("Tahoma", 12, FontStyle.Bold), Brushes.White, PSAL.playerXY.X, PSAL.playerXY.Y - (int)(playerSize * 1.35), format);
                    }
                    else
                    {
                        g.DrawString(PSAL.Position, new Font("Tahoma", 12, FontStyle.Bold), Brushes.White, PSAL.playerXY.X, PSAL.playerXY.Y + (int)(playerSize * 1.35), format);
                    }
                }
            }
            catch
            {
                g.FillEllipse(sb, (int)(PSAL.playerXY.X - (playerSize / 2)), (int)(PSAL.playerXY.Y - (playerSize / 2)), playerSize, playerSize);
            }

            sb.Dispose();
        }

        public static XY MoveDistDirToXY(int dist, int dir, double angleRatio)
        {
            double angle = Math.PI * (dir / angleRatio) / 180.0;
            double sinAngle = Math.Sin(angle);
            double cosAngle = Math.Cos(angle);

            if (dist > 128)
            {
                dist = (int)(dist * .5);
            }

            return new XY
            {
                X = (int)(cosAngle * (dist / .8)),
                Y = (int)(sinAngle * (dist / .8)) * -1
            };
        }

        public static XY GetOptionOffset(int code, double angleRatio)
        {
            if (code == 0)     //Curl left
            {
                return MoveDistDirToXY(32, (int)(225 * angleRatio), angleRatio);
            }

            if (code == 1)    //Curl right
            {
                return MoveDistDirToXY(32, (int)(-45 * angleRatio), angleRatio);
            }

            if (code == 2)    //Post right
            {
                return MoveDistDirToXY(90, (int)(45 * angleRatio), angleRatio);
            }

            if (code == 3)    //Corner left
            {
                return MoveDistDirToXY(90, (int)(135 * angleRatio), angleRatio);
            }

            if (code == 5)     //Slant right
            {
                return MoveDistDirToXY(90, (int)(33 * angleRatio), angleRatio);
            }

            if (code == 6)    //Fade/Streak left
            {
                return MoveDistDirToXY(90, (int)(93 * angleRatio), angleRatio);
            }

            if (code == 7)    //Slant left
            {
                return MoveDistDirToXY(90, (int)(147 * angleRatio), angleRatio);
            }

            if (code == 8)    //Fade/Streak right
            {
                return MoveDistDirToXY(90, (int)(87 * angleRatio), angleRatio);
            }

            if (code == 9)     //In/Out right
            {
                return MoveDistDirToXY(90, (int)(0 * angleRatio), angleRatio);
            }

            if (code == 10)    //In/Out left
            {
                return MoveDistDirToXY(90, (int)(180 * angleRatio), angleRatio);
            }

            if (code == 11)   //Fade left
            {
                return MoveDistDirToXY(90, (int)(93 * angleRatio), angleRatio);
            }

            if (code == 12)    //Hitch left
            {
                return MoveDistDirToXY(32, (int)(225 * angleRatio), angleRatio);
            }

            if (code == 13)    //Hitch right
            {
                return MoveDistDirToXY(32, (int)(-45 * angleRatio), angleRatio);
            }

            if (code == 15)   //180 Partial
            {
                return MoveDistDirToXY(32, (int)(270 * angleRatio), angleRatio);
            }

            if (code == 16)   //180 Partial
            {
                return MoveDistDirToXY(32, (int)(270 * angleRatio), angleRatio);
            }

            if (code == 17)   //Drag right
            {
                return MoveDistDirToXY(90, (int)(3 * angleRatio), angleRatio);
            }

            if (code == 18)   //Drag left
            {
                return MoveDistDirToXY(90, (int)(177 * angleRatio), angleRatio);
            }

            if (code == 19)   //Hitch right
            {
                return MoveDistDirToXY(32, (int)(-45 * angleRatio), angleRatio);
            }

            if (code == 20)   //Hitch left
            {
                return MoveDistDirToXY(32, (int)(225 * angleRatio), angleRatio);
            }
            return new XY { X = 0, Y = 0 };
        }

        public static Point[] XYtoPoint(List<XY> xy)
        {
            Point[] points = new Point[xy.Count];

            for (int i = 0; i < xy.Count; i++)
            {
                points[i].X = xy[i].X;
                points[i].Y = xy[i].Y;
            }

            return points;
        }

        public static void ResizeDataGrid(DataGridView dgv)
        {
            int i = 0;
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                if (c.Visible) i += c.Width;
            }
            dgv.Width = i + 3;
            try { dgv.Height = (dgv.GetRowDisplayRectangle(0, false).Height * (dgv.Rows.Count + 1)) + 1; }
            catch { }
        }
    }
}