using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model.Concepts
{
    public class PermitPair : BasePair, IEntity
    {
        #region Variable

        public int value = 0;

        #endregion

        #region Constructor

        public PermitPair()
            :this(0, 0)
        { }

        public PermitPair(int from, int to)
            : base(from, to)
        {
        }
        #endregion

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public bool IsFilled
        {
            get;
            set;
        }

        /// <summary>
        /// «ê— »—«»— "œ—” " »«‘œ »œÌ‰ „⁄‰«”  òÂ «Ì‰ “ÊÃ „— » »’Ê— 
        /// ’Ê—Ì œ—Ã ê—œÌœÂ «”  Ê «» œ« Ê«‰ Â« ‰œ«—œ
        /// </summary>
        public bool IsEpty
        {
            get
            {
                if (this.From == 0 && this.To == 0)
                {
                    return true;
                }
                return false;
            }   
        }

        public decimal PreCardID
        {
            get;
            set;
        }

        public decimal RequestID { get; set; }

        public bool IsApplyedOnTraffic { get; set; }

        public override int Value
        {
            get
            {
                if (this.value == -1000)
                {
                    if (this.From <= this.To)
                    {
                        return this.To - this.From;
                    }
                    else
                    {
                        throw new BaseException("„ﬁœ«— «“ »“—ê — «“ „ﬁœ«—  « „Ì »«‘œ", this.GetType().ToString() + ".value");
                    }
                }

                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public virtual Permit Permit { get; set; }

        public virtual Precard Precard { get; set; }

        #endregion

    }
}
