using System;

namespace FTN.Common
{
    public enum UnitMultiplier
    {


        c,


        d,


        G,


        k,


        m,


        M,


        micro,


        n,


        none,


        p,


        T,
    }

    public enum UnitSymbol
    {


        A,


        deg,


        degC,


        F,


        g,


        h,


        H,


        Hz,


        J,


        m,


        m2,


        m3,


        min,


        N,


        none,


        ohm,


        Pa,


        rad,


        s,


        S,


        V,


        VA,


        VAh,


        VAr,


        VArh,


        W,


        Wh,
    }

    public enum SwitchState
    {

        /// Switch is closed.
        close,

        /// Switch is open.
        open,
    }

    public enum CurveStyle
    {


        constantYValue,

        formula,

        rampYValue,

        straightLineYValues,
    }

}
