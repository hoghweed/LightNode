﻿using LightNode.Core;
using System;
using System.Collections.Generic;

namespace LightNode.Server
{
    public class LightNodeOptions
    {
        public AcceptVerbs DefaultAcceptVerb { get; private set; }
        public IContentFormatter DefaultFormatter { get; private set; }
        public IContentFormatter[] SpecifiedFormatters { get; private set; }

        public bool UseOtherMiddleware { get; set; }
        public bool ParameterStringImplicitNullAsDefault { get; set; }
        public bool ParameterEnumAllowsFieldNameParse { get; set; }

        /// <summary>
        /// <pre>Use buffering when content formatter serialize, Default is BufferAndWrite.</pre>
        /// <pre>If you use top level stream buffering, set to DirectWrite for avoid double buffering.</pre>
        /// </summary>
        public StreamWriteOption StreamWriteOption { get; set; }

        public ErrorHandlingPolicy ErrorHandlingPolicy { get; set; }

        public LightNodeFilterCollection Filters { get; private set; }

        public LightNodeOptions(AcceptVerbs defaultAcceptVerb, IContentFormatter defaultFormatter, params IContentFormatter[] specifiedFormatters)
        {
            DefaultAcceptVerb = defaultAcceptVerb;
            DefaultFormatter = defaultFormatter;
            SpecifiedFormatters = specifiedFormatters;
            UseOtherMiddleware = false;
            ParameterStringImplicitNullAsDefault = false;
            ParameterEnumAllowsFieldNameParse = false;
            StreamWriteOption = Server.StreamWriteOption.BufferAndWrite;
            ErrorHandlingPolicy = Server.ErrorHandlingPolicy.ThrowException;
            Filters = new LightNodeFilterCollection();
        }

        public LightNodeOptions ConfigureWith(Action<LightNodeOptions> @this)
        {
            @this(this);
            return this;
        }
    }

    [Flags]
    public enum AcceptVerbs
    {
        Get = 1,
        Post = 2
    }

    public enum StreamWriteOption
    {
        BufferAndWrite = 0,
        BufferAndAsynchronousWrite = 1,
        DirectWrite = 2
    }

    public enum ErrorHandlingPolicy
    {
        /// <summary>Do Nothing, throw next pipeline.</summary>
        ThrowException = 0,
        /// <summary>Response StatusCode is 500.</summary>
        ReturnInternalServerError = 1,
        /// <summary>Response StatusCode is 500 and ResponseBody includes error details for debugging.</summary>
        ReturnInternalServerErrorIncludeErrorDetails = 2
    }
}