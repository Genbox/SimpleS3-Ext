﻿using System;
using System.Globalization;
using System.Text;
using Genbox.SimpleS3.Abstracts;
using Genbox.SimpleS3.Abstracts.Authentication;
using Genbox.SimpleS3.Core.Internal.Constants;
using Genbox.SimpleS3.Core.Internal.Extensions;
using Genbox.SimpleS3.Core.Internal.Helpers;
using Genbox.SimpleS3.Utils;
using Microsoft.Extensions.Logging;

namespace Genbox.SimpleS3.Core.Authentication
{
    public class ChunkedSignatureBuilder : IChunkedSignatureBuilder
    {
        private readonly ISigningKeyBuilder _keyBuilder;
        private readonly ILogger<ChunkedSignatureBuilder> _logger;
        private readonly IScopeBuilder _scopeBuilder;

        public ChunkedSignatureBuilder(ISigningKeyBuilder keyBuilder, IScopeBuilder scopeBuilder, ILogger<ChunkedSignatureBuilder> logger)
        {
            _keyBuilder = keyBuilder;
            _scopeBuilder = scopeBuilder;
            _logger = logger;
        }

        public byte[] CreateChunkSignature(IRequest request, byte[] previousSignature, byte[] content, int contentLength)
        {
            Validator.RequireNotNull(request, nameof(request));

            _logger.LogTrace("Creating chunk signature for {RequestId}", request.RequestId);

            string stringToSign = CreateStringToSign(request.Date, _scopeBuilder.CreateScope("s3", request.Date), previousSignature, content, contentLength);
            byte[] signature = CreateSignature(request.Date, stringToSign);

            _logger.LogDebug("Chunk signature: {signature}", signature);
            return signature;
        }

        internal string CreateStringToSign(DateTimeOffset dateTime, string scope, byte[] previousSignature, byte[] content, int contentLength)
        {
            _logger.LogTrace("Creating chunked StringToSign");

            StringBuilder sb = new StringBuilder(512);
            sb.Append(SigningConstants.ChunkedAlgorithmTag).Append(SigningConstants.Newline);
            sb.Append(dateTime.ToString(DateTimeFormats.Iso8601DateTime, DateTimeFormatInfo.InvariantInfo)).Append(SigningConstants.Newline);
            sb.Append(scope).Append(SigningConstants.Newline);
            sb.Append(previousSignature.HexEncode()).Append(SigningConstants.Newline);
            sb.Append("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855").Append(SigningConstants.Newline);
            sb.Append(CryptoHelper.Sha256Hash(content, contentLength).HexEncode());

            string sts = sb.ToString();
            _logger.LogDebug("Chunked StringToSign: {StringToSign}", sts);
            return sts;
        }

        internal byte[] CreateSignature(DateTimeOffset date, string stringToSign)
        {
            //See https://docs.aws.amazon.com/AmazonS3/latest/API/sig-v4-header-based-auth.html
            //Consists of:
            // Hmac-Sha256(SigningKey, StringToSign)

            return CryptoHelper.HmacSign(Encoding.UTF8.GetBytes(stringToSign), _keyBuilder.CreateSigningKey(date, "s3"));
        }
    }
}