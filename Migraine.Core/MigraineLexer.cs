﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Migraine.Core
{
    public class MigraineLexer
    {
        private GenericLexer lexer;

        public MigraineLexer()
        {
            var comparisonOperatorsRegex = new Regex(@"==|>=|<=|>|<");
            var operatorRegex = new Regex(@"[\*/\+\-=]");
            var symbolRegex = new Regex(@"[\(\)\{\},]");
            var whiteSpaceRegex = new Regex(@"[\s]+");
            var terminatorRegex = new Regex(@";");
            var numberRegex = new Regex(@"(\d)+(\.[\d]+)?");
            var identifierRegex = new Regex(@"[A-Za-z0-9_]+");

            var tokenDefinitions = new List<TokenDefinition>();

            tokenDefinitions.Add(new TokenDefinition(comparisonOperatorsRegex, TokenType.ComparisonOperator));
            tokenDefinitions.Add(new TokenDefinition(operatorRegex, TokenType.Operator));
            tokenDefinitions.Add(new TokenDefinition(symbolRegex, TokenType.Symbol));
            tokenDefinitions.Add(new TokenDefinition(whiteSpaceRegex, TokenType.Whitespace));
            tokenDefinitions.Add(new TokenDefinition(numberRegex, TokenType.Number));
            tokenDefinitions.Add(new TokenDefinition(identifierRegex, TokenType.Identifier));
            tokenDefinitions.Add(new TokenDefinition(terminatorRegex, TokenType.Terminator));

            lexer = new GenericLexer(tokenDefinitions);
        }

        public TokenStream Tokenize(String input)
        {
            input = PreProcessInput(input);

            var stream = new TokenStream();

            var tokens = lexer.Tokenize(input) as List<Token>;
            //Ignore all whitespace
            tokens.Where(t => t.Type != TokenType.Whitespace).ToList()
                .ForEach(t => stream.Add(t));

            return stream;
        }

        private String PreProcessInput(String input)
        {
            return input.Replace("\r\n", "\n").Replace("\r", "\n");
        }
    }
}
