﻿namespace Genbox.SimpleS3.Extensions.AmazonS3;

/// <summary>The AWS regions. See https://docs.aws.amazon.com/en_pv/general/latest/gr/rande.html#s3_region for more info</summary>
public enum AmazonS3Region
{
    /// <summary>Do not use this value</summary>
    Unknown = 0,

    /// <summary>Africa (Cape Town)</summary>
    AfSouth1,

    /// <summary>Asia Pacific (Hong Kong)</summary>
    ApEast1,

    /// <summary>Asia Pacific (Tokyo)</summary>
    ApNorthEast1,

    /// <summary>Asia Pacific (Seoul)</summary>
    ApNorthEast2,

    /// <summary>Asia Pacific (Osaka-Local)</summary>
    ApNorthEast3,

    /// <summary>Asia Pacific (Mumbai)</summary>
    ApSouth1,

    /// <summary>Asia Pacific (Hyderabad)</summary>
    ApSouth2,

    /// <summary>Asia Pacific (Singapore)</summary>
    ApSouthEast1,

    /// <summary>Asia Pacific (Sydney)</summary>
    ApSouthEast2,

    /// <summary>Asia Pacific (Jakarta)</summary>
    ApSouthEast3,

    /// <summary>Asia Pacific (Melbourne)</summary>
    ApSouthEast4,

    /// <summary>Asia Pacific (Malaysia)</summary>
    ApSouthEast5,

    /// <summary>Canada (Central)</summary>
    CaCentral1,

    /// <summary>Canada West (Calgary)</summary>
    CaWest1,

    /// <summary>China (Beijing)</summary>
    CnNorth1,

    /// <summary>China (Ningxia)</summary>
    CnNorthWest1,

    /// <summary>EU (Frankfurt)</summary>
    EuCentral1,

    /// <summary>Europe (Zurich)</summary>
    EuCentral2,

    /// <summary>EU (Stockholm)</summary>
    EuNorth1,

    /// <summary>EU (Ireland)</summary>
    EuWest1,

    /// <summary>EU (London)</summary>
    EuWest2,

    /// <summary>EU (Paris)</summary>
    EuWest3,

    /// <summary>Europe (Milan)</summary>
    EuSouth1,

    /// <summary>Europe (Spain)</summary>
    EuSouth2,

    /// <summary>Middle East (UAE)</summary>
    MeCentral1,

    /// <summary>Middle East (Bahrain)</summary>
    MeSouth1,

    /// <summary>South America (Sao Paulo)</summary>
    SaEast1,

    /// <summary>US East (N. Virginia)</summary>
    UsEast1,

    /// <summary>US East (Ohio)</summary>
    UsEast2,

    /// <summary>Israel (Tel Aviv)</summary>
    IlCentral1,

    /// <summary>US West (N. California)</summary>
    UsWest1,

    /// <summary>US West (Oregon)</summary>
    UsWest2,

    /// <summary>AWS GovCloud (US-East)</summary>
    UsGovEast1,

    /// <summary>AWS GovCloud (US-West)</summary>
    UsGovWest1
}