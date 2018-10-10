// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Toolkit.Services.Core;
using Microsoft.Win32.SafeHandles;
using static Microsoft.Toolkit.Services.PlatformSpecific.NetFramework.PasswordManagerNativeMethods;

namespace Microsoft.Toolkit.Services.PlatformSpecific.NetFramework
{
    internal class NetFrameworkPasswordManager : IPasswordManager
    {
        public void Store(string resource, PasswordCredential credential)
        {
            // Validations.
            byte[] byteArray = Encoding.Unicode.GetBytes(credential.Password);
            if (byteArray.Length > 512)
            {
                throw new ArgumentOutOfRangeException("The secret message has exceeded 512 bytes.");
            }

            // Go ahead with what we have are stuff it into the CredMan structures.
            Credential cred = new Credential
            {
                TargetName = resource,
                UserName = credential.UserName,
                CredentialBlob = credential.Password,
                CredentialBlobSize = (uint)Encoding.Unicode.GetBytes(credential.Password).Length,
                AttributeCount = 0,
                Attributes = IntPtr.Zero,
                Comment = null,
                TargetAlias = null,
                Type = CRED_TYPE.GENERIC,
                Persist = CRED_PERSIST.LOCAL_MACHINE
            };
            NativeCredential ncred = NativeCredential.GetNativeCredential(cred);

            // Write the info into the CredMan storage.
            bool written = CredWrite(ref ncred, 0);
            int lastError = Marshal.GetLastWin32Error();
            if (!written)
            {
                string message = "CredWrite failed with the error code " + lastError.ToString();
                throw new InvalidOperationException(message);
            }
        }

        public PasswordCredential Get(string key)
        {
            IntPtr nCredPtr;
            PasswordCredential passCred = new PasswordCredential();

            int lastError = Marshal.GetHRForLastWin32Error();

            if (!CredRead(key, CRED_TYPE.GENERIC, 0, out nCredPtr))
            {
                return null;
            }
            else
            {
                CriticalCredentialHandle critCred = new CriticalCredentialHandle(nCredPtr);

                Credential credential = critCred.GetCredential();
                passCred.UserName = credential.UserName;
                passCred.Password = credential.CredentialBlob;
            }

            return passCred;
        }

        public void Remove(string key)
        {
            CredDelete(key, CRED_TYPE.GENERIC, 0);
        }
    }
}
