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
        private GenericLexer _lexer;

        public MigraineLexer()
        {
            string operatorPattern = String.Format("[{0}]", Regex.Escape("()*/+-"));

            var operatorRegex = new Regex(operatorPattern);
            var whiteSpaceRegex = new Regex(@"[\s]+");
            var numberRegex = new Regex(@"(\d)+(\.[\d])*");
            var identifierRegex = new Regex(@"[A-Za-z0-9_]+");

            var tokenDefinitions = new List<TokenDefinition>();

            tokenDefinitions.Add(new TokenDefinition(operatorRegex, TokenType.Operator));
            tokenDefinitions.Add(new TokenDefinition(whiteSpaceRegex, TokenType.Whitespace));
            tokenDefinitions.Add(new TokenDefinition(numberRegex, TokenType.Number));
            tokenDefinitions.Add(new TokenDefinition(identifierRegex, TokenType.Identifier));

            _lexer = new GenericLexer(tokenDefinitions);
        }

        public Queue<Token> Tokenize(String input)
        {
            var queue = new Queue<Token>();

            var tokens = _lexer.Tokenize(input) as List<Token>;
            tokens.Where(t => t.Type != TokenType.Whitespace).ToList()
                .ForEach(t => queue.Enqueue(t));

            return queue;
        }
    }
}