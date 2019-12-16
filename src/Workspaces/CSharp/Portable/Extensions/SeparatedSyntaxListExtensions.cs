﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

#nullable enable

using System.Collections.Generic;

namespace Microsoft.CodeAnalysis.CSharp.Extensions
{
    internal static class SeparatedSyntaxListExtensions
    {
        public static SeparatedSyntaxList<T> InsertRangeWithTrailingSeparator<T>(this SeparatedSyntaxList<T> separatedList, int index, IEnumerable<T> nodes)
            where T : SyntaxNode
        {
            // Could be implemented more efficiently by parameterizing the implementation of SeparatedSyntaxList<>.InsertRange
            var newList = separatedList.InsertRange(index, nodes);

            if (index >= separatedList.Count)
            {
                var nodesAndTokens = newList.GetWithSeparators();

                if (nodesAndTokens.Last().IsNode)
                {
                    return SyntaxFactory.SeparatedList<T>(nodesAndTokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken)));
                }
            }

            return newList;
        }
    }
}
