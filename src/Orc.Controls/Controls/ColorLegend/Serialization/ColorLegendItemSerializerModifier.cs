﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorLegendItemSerializerModifier.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls
{
    using System;
    using System.Windows.Media;
    using Catel.Logging;
    using Catel.Runtime.Serialization;

    public class ColorLegendItemSerializerModifier : SerializerModifierBase<ColorLegendItem>
    {
        #region Constants
        private const char ArgbSeparator = ';';
        #endregion

        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region Methods
        public override void SerializeMember(ISerializationContext context, MemberValue memberValue)
        {
            if (string.Equals(nameof(ColorLegendItem.Color), memberValue.Name))
            {
                var color = (Color)memberValue.Value;
                memberValue.Value = $"{color.A}{ArgbSeparator}{color.R}{ArgbSeparator}{color.G}{ArgbSeparator}{color.B}";
            }

            base.SerializeMember(context, memberValue);
        }

        public override void DeserializeMember(ISerializationContext context, MemberValue memberValue)
        {
            if (string.Equals(nameof(ColorLegendItem.Color), memberValue.Name))
            {
                var color = memberValue.Value as string;

                try
                {
                    if (!string.IsNullOrEmpty(color))
                    {
                        var colorParts = color.Split(ArgbSeparator);

                        memberValue.Value = Color.FromArgb(Convert.ToByte(colorParts[0]), Convert.ToByte(colorParts[1]),
                            Convert.ToByte(colorParts[2]), Convert.ToByte(colorParts[3]));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Cannot modify '{0}' to a color", color);
                }
            }

            base.DeserializeMember(context, memberValue);
        }
        #endregion
    }
}
