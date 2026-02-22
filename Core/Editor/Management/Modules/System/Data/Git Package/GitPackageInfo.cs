/*
 * Copyright (c) 2025 Carter Games
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Linq;

namespace CarterGames.Cart.Crate
{
    /// <summary>
    /// Handles data for an external git package that can be imported.
    /// </summary>
    public sealed class GitPackageInfo
    {
        public string displayName;
        public string packageName;
        public string packageUrl;
        public Version packageVersion;
        public string[] altPackageNames;

        public string CompletePackageUrl
        {
            get
            {
                if (packageVersion == null)
                {
                    return packageUrl;
                }
                else
                {
                    return $"{packageUrl}#{packageVersion}";
                }
            }
        }
        
        
        public GitPackageInfo(string displayName, string packageName, string packageUrl)
        {
            this.displayName = displayName;
            this.packageName = packageName;
            this.packageUrl = packageUrl;
        }
        
        
        public GitPackageInfo(string displayName, string packageName, string packageUrl, Version version)
        {
            this.displayName = displayName;
            this.packageName = packageName;
            this.packageUrl = packageUrl;
            packageVersion = version;
        }
        
        public GitPackageInfo(string displayName, string[] packageNames, string packageUrl, Version version)
        {
            this.displayName = displayName;
            packageName = packageNames.First();
            altPackageNames = packageNames;
            this.packageUrl = packageUrl;
            packageVersion = version;
        }
    }
}