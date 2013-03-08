﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine.Core.Tests
{
    [TestFixture]
    public class TokenStreamTests
    {
        private TokenStream _tokenStream;
        private Token _token;

        [SetUp]
        public void SetUp()
        {
            _tokenStream = new TokenStream();
            _token = new Token("5", TokenType.Number);
            _tokenStream.Add(_token);
        }

        [Test]
        public void StreamCountRaiseWhenAdd()
        {
            Assert.AreEqual(1, _tokenStream.Count);
            _tokenStream.Add(_token);
            Assert.AreEqual(2, _tokenStream.Count);
        }

        [Test]
        public void StreamCanConsumeAnyToken()
        {
            Assert.IsTrue(_tokenStream.Consume());
        }

        [Test]
        public void StreamCanConsumeTokenByValue()
        {
            Assert.IsTrue(_tokenStream.Consume("5"));
        }

        [Test]
        public void StreamCanConsumeTokenByType()
        {
            Assert.IsTrue(_tokenStream.Consume(TokenType.Number));
        }

        [Test]
        public void CanAccessCurrentToken()
        {
            Assert.AreEqual(_token, _tokenStream.CurrentToken);
        }

        [Test]
        public void CurrentTokenIsNullIfStreamEmpty()
        {
            _tokenStream.Consume();
            Assert.IsNull(_tokenStream.CurrentToken);
        }

        [Test]
        public void ConsumeShouldReduceCountByOne()
        {
            _tokenStream.Consume();
            Assert.AreEqual(0, _tokenStream.Count);
        }

        [Test]
        public void ConsumeWrongTypeReturnsFalse()
        {
            Assert.IsFalse(_tokenStream.Consume(TokenType.Operator));
        }

        [Test]
        public void ConsumeWrongValueReturnsFalse()
        {
            Assert.IsFalse(_tokenStream.Consume("+"));
        }

        [Test]
        public void ConsumeOnEmptyStreamThrowsException()
        {
            _tokenStream.Consume();
            Assert.Throws(typeof(TokenStreamEmptyException), () => _tokenStream.Consume());
        }

        [Test]
        public void CanAccessConsumedToken()
        {
            var currentToken = _tokenStream.CurrentToken;
            _tokenStream.Consume();
            Assert.AreEqual(currentToken, _tokenStream.ConsumedToken);
        }

        [Test]
        public void ExpectBehavesAsConsume()
        {
            var currentToken = _tokenStream.CurrentToken;
            Assert.IsTrue(_tokenStream.Expect(TokenType.Number));

            _tokenStream.Add(currentToken);
            Assert.IsTrue(_tokenStream.Expect("5"));
        }

        [Test]
        public void ExpectThrowsExceptionIfWrongTypeOrValue()
        {
            Assert.Throws<ExpectedTokenException>(() => _tokenStream.Expect("+"));
            Assert.Throws<ExpectedTokenException>(() => _tokenStream.Expect(TokenType.Operator));
        }
    }
}
