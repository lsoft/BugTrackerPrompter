//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Visual Studio SDK license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************

using System.Text.RegularExpressions;

namespace BugTrackerPrompter.Support
{
    public readonly struct Regex2
    {
        private readonly Regex _regex;
        private readonly Func<bool> _isEnabled;

        public bool TryGetRegex(out Regex result)
        {
            var isEnabled = _isEnabled();
            if (isEnabled)
            {
                result = _regex;
                return true;
            }

            result = null;
            return false;
        }

        public Regex2(
            Regex regex,
            Func<bool> isEnabled
            )
        {
            _regex = regex;
            _isEnabled = isEnabled;
        }
    }
}
