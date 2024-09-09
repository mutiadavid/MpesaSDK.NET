using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace MpesaSDK.NET.Helpers
{
    /// <summary>
    /// Password Encryption helper class
    /// </summary>
    public static class Encrypt
    {
        //TODO: Implement 
        /// <summary>
        /// Mpesa Pass encryption.
        /// </summary>
        /// <param name="pass">password to be encrypted with MPesa Cert</param>
        /// <returns></returns>
        public static string ToMpesaSecurityCredential(this string pass)
        {
            string certPulicKey = ((RSA)new X509Certificate2(Convert.FromBase64String(certKey)).PublicKey.Key).ToXmlString2(false);
            byte[] bytes = Encoding.UTF8.GetBytes(pass);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString2(certPulicKey);
                    return Convert.ToBase64String(rsa.Encrypt(bytes, false));
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private static void FromXmlString2(this RSACryptoServiceProvider rsa, string xmlString)
        {
            FromXmlStringImpl(rsa, xmlString);
        }

        private static void FromXmlStringImpl(RSACryptoServiceProvider rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            if (!xmlDocument.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                throw new InvalidOperationException("Invalid XML RSA key.");
            }

            foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "D":
                        parameters.D = Convert.FromBase64String(childNode.InnerText);
                        continue;
                    case "DP":
                        parameters.DP = Convert.FromBase64String(childNode.InnerText);
                        continue;
                    case "DQ":
                        parameters.DQ = Convert.FromBase64String(childNode.InnerText);
                        continue;
                    case "Exponent":
                        parameters.Exponent = Convert.FromBase64String(childNode.InnerText);
                        continue;
                    case "InverseQ":
                        parameters.InverseQ = Convert.FromBase64String(childNode.InnerText);
                        continue;
                    case "Modulus":
                        parameters.Modulus = Convert.FromBase64String(childNode.InnerText);
                        continue;
                    case "P":
                        parameters.P = Convert.FromBase64String(childNode.InnerText);
                        continue;
                    case "Q":
                        parameters.Q = Convert.FromBase64String(childNode.InnerText);
                        continue;
                    default:
                        throw new InvalidOperationException("Unknown node name: " + childNode.Name);
                }
            }
            rsa.ImportParameters(parameters);
        }

        private static string ToXmlString2(this RSA rsa, bool includePrivateParameters = false)
        {
            RSAParameters rsaParameters = rsa.ExportParameters(includePrivateParameters);
            if (!includePrivateParameters)
            {
                return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>", Convert.ToBase64String(rsaParameters.Modulus), Convert.ToBase64String(rsaParameters.Exponent));
            }

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>", Convert.ToBase64String(rsaParameters.Modulus), Convert.ToBase64String(rsaParameters.Exponent), Convert.ToBase64String(rsaParameters.P), Convert.ToBase64String(rsaParameters.Q), Convert.ToBase64String(rsaParameters.DP), Convert.ToBase64String(rsaParameters.DQ), Convert.ToBase64String(rsaParameters.InverseQ), Convert.ToBase64String(rsaParameters.D));
        }



        private const string certKey = @"MIIGkzCCBXugAwIBAgIKXfBp5gAAAD+hNjANBgkqhkiG9w0BAQsFADBbMRMwEQYK
CZImiZPyLGQBGRYDbmV0MRkwFwYKCZImiZPyLGQBGRYJc2FmYXJpY29tMSkwJwYD
VQQDEyBTYWZhcmljb20gSW50ZXJuYWwgSXNzdWluZyBDQSAwMjAeFw0xNzA0MjUx
NjA3MjRaFw0xODAzMjExMzIwMTNaMIGNMQswCQYDVQQGEwJLRTEQMA4GA1UECBMH
TmFpcm9iaTEQMA4GA1UEBxMHTmFpcm9iaTEaMBgGA1UEChMRU2FmYXJpY29tIExp
bWl0ZWQxEzARBgNVBAsTClRlY2hub2xvZ3kxKTAnBgNVBAMTIGFwaWdlZS5hcGlj
YWxsZXIuc2FmYXJpY29tLmNvLmtlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIB
CgKCAQEAoknIb5Tm1hxOVdFsOejAs6veAai32Zv442BLuOGkFKUeCUM2s0K8XEsU
t6BP25rQGNlTCTEqfdtRrym6bt5k0fTDscf0yMCoYzaxTh1mejg8rPO6bD8MJB0c
FWRUeLEyWjMeEPsYVSJFv7T58IdAn7/RhkrpBl1dT7SmIZfNVkIlD35+Cxgab+u7
+c7dHh6mWguEEoE3NbV7Xjl60zbD/Buvmu6i9EYz+27jNVPI6pRXHvp+ajIzTSsi
eD8Ztz1eoC9mphErasAGpMbR1sba9bM6hjw4tyTWnJDz7RdQQmnsW1NfFdYdK0qD
RKUX7SG6rQkBqVhndFve4SDFRq6wvQIDAQABo4IDJDCCAyAwHQYDVR0OBBYEFG2w
ycrgEBPFzPUZVjh8KoJ3EpuyMB8GA1UdIwQYMBaAFOsy1E9+YJo6mCBjug1evuh5
TtUkMIIBOwYDVR0fBIIBMjCCAS4wggEqoIIBJqCCASKGgdZsZGFwOi8vL0NOPVNh
ZmFyaWNvbSUyMEludGVybmFsJTIwSXNzdWluZyUyMENBJTIwMDIsQ049U1ZEVDNJ
U1NDQTAxLENOPUNEUCxDTj1QdWJsaWMlMjBLZXklMjBTZXJ2aWNlcyxDTj1TZXJ2
aWNlcyxDTj1Db25maWd1cmF0aW9uLERDPXNhZmFyaWNvbSxEQz1uZXQ/Y2VydGlm
aWNhdGVSZXZvY2F0aW9uTGlzdD9iYXNlP29iamVjdENsYXNzPWNSTERpc3RyaWJ1
dGlvblBvaW50hkdodHRwOi8vY3JsLnNhZmFyaWNvbS5jby5rZS9TYWZhcmljb20l
MjBJbnRlcm5hbCUyMElzc3VpbmclMjBDQSUyMDAyLmNybDCCAQkGCCsGAQUFBwEB
BIH8MIH5MIHJBggrBgEFBQcwAoaBvGxkYXA6Ly8vQ049U2FmYXJpY29tJTIwSW50
ZXJuYWwlMjBJc3N1aW5nJTIwQ0ElMjAwMixDTj1BSUEsQ049UHVibGljJTIwS2V5
JTIwU2VydmljZXMsQ049U2VydmljZXMsQ049Q29uZmlndXJhdGlvbixEQz1zYWZh
cmljb20sREM9bmV0P2NBQ2VydGlmaWNhdGU/YmFzZT9vYmplY3RDbGFzcz1jZXJ0
aWZpY2F0aW9uQXV0aG9yaXR5MCsGCCsGAQUFBzABhh9odHRwOi8vY3JsLnNhZmFy
aWNvbS5jby5rZS9vY3NwMAsGA1UdDwQEAwIFoDA9BgkrBgEEAYI3FQcEMDAuBiYr
BgEEAYI3FQiHz4xWhMLEA4XphTaE3tENhqCICGeGwcdsg7m5awIBZAIBDDAdBgNV
HSUEFjAUBggrBgEFBQcDAgYIKwYBBQUHAwEwJwYJKwYBBAGCNxUKBBowGDAKBggr
BgEFBQcDAjAKBggrBgEFBQcDATANBgkqhkiG9w0BAQsFAAOCAQEAC/hWx7KTwSYr
x2SOyyHNLTRmCnCJmqxA/Q+IzpW1mGtw4Sb/8jdsoWrDiYLxoKGkgkvmQmB2J3zU
ngzJIM2EeU921vbjLqX9sLWStZbNC2Udk5HEecdpe1AN/ltIoE09ntglUNINyCmf
zChs2maF0Rd/y5hGnMM9bX9ub0sqrkzL3ihfmv4vkXNxYR8k246ZZ8tjQEVsKehE
dqAmj8WYkYdWIHQlkKFP9ba0RJv7aBKb8/KP+qZ5hJip0I5Ey6JJ3wlEWRWUYUKh
gYoPHrJ92ToadnFCCpOlLKWc0xVxANofy6fqreOVboPO0qTAYpoXakmgeRNLUiar
0ah6M/q/KA==";

    }
}

