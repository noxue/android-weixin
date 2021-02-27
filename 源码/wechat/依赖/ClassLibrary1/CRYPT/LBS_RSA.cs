namespace CRYPT
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;

    public static class LBS_RSA
    {
        private static byte[] LBSPubKey = "E8AA7E72F02D43D20F0B56BFCB4A608188D242E1374FB076BEA9606EABFC4486FD35315CC8A0DE5573C519D2A2E30DE5A1E3AD8EBB63ECE84E8ED42F95493071B4BAE9CC13B107F88050A830E707B81BC90D8A3795BA656BA3F028BD47AA50946B31591204C6E9FCED5665F88AEDB40DD14C45CE941ED23F0767D8701969E349".ToByteArray(0x10, 2);

        public static byte[] EncodeKey(byte[] data)
        {
            using (RSA rsa = RSA.Create())
            {
                byte[] first = new byte[0];
                RSAParameters parameters = new RSAParameters {
                    Exponent = "010001".ToByteArray(0x10, 2),
                    Modulus = LBSPubKey
                };
                rsa.ImportParameters(parameters);
                int num = rsa.KeySize / 8;
                if (data.Length > (num - 12))
                {
                    int num2 = (data.Length / (num - 12)) + (((data.Length % (num - 12)) == 0) ? 0 : 1);
                    for (int i = 0; i < num2; i++)
                    {
                        int length = num - 12;
                        if (i == (num2 - 1))
                        {
                            length = data.Length - (i * length);
                        }
                        byte[] buffer2 = data.Copy<byte>(i * (num - 12), length);
                        first = first.Concat<byte>(rsa.Encrypt(buffer2, RSAEncryptionPadding.Pkcs1)).ToArray<byte>();
                    }
                    return first;
                }
                return rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            }
        }
    }
}

