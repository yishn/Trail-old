/*
 * Code from the article 
 * [A Better File.Copy Replacement](http://www.informit.com/guides/content.aspx?g=dotnet&seqNum=854)
 * by Jim Mischel
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Mischel.IO {
    [Flags]
    public enum CopyFileOptions {
        None = 0,
        AllowDecryptedDestination = 0x00000008,
        CopySumlink = 0x00000800,
        FailIfExists = 0x00000001,
        NoBuffering = 0x00001000,
        OpenSourceForWrite = 0x00000004,
        Restartable = 0x00000002
    }

    public enum CopyCallbackReason {
        ChunkFinished = 0,
        StreamSwitch = 1
    }

    public enum ProgressCallbackResult {
        Continue = 0,
        Cancel = 1,
        Stop = 2,
        Quiet = 3
    }

    public class CopyProgressArgs {
        public readonly Int64 TotalFileSize;
        public readonly Int64 TotalBytesTransferred;
        public readonly Int64 StreamSize;
        public readonly Int64 StreamBytesTransferred;
        public readonly Int32 StreamNumber;
        public readonly Int32 CallbackReason;
        public readonly IntPtr SourceHandle;
        public readonly IntPtr DestinationHandle;
        public readonly Object UserData;
        public ProgressCallbackResult Result { get; set; }
        public CopyProgressArgs(Int64 fsize, Int64 xferBytes, Int64 strmSize, Int64 strmXferBytes,
            Int32 strmNum, Int32 reason, IntPtr srcHandle, IntPtr destHandle, Object uData) {
            TotalFileSize = fsize;
            TotalBytesTransferred = xferBytes;
            StreamSize = strmSize;
            StreamBytesTransferred = strmXferBytes;
            StreamNumber = strmNum;
            CallbackReason = reason;
            SourceHandle = srcHandle;
            DestinationHandle = destHandle;
            UserData = uData;
        }
    }

    public delegate void CopyProgressDelegate(CopyProgressArgs e);

    public class FileUtil {
        public delegate Int32 APICopyProgressRoutine(
            Int64 TotalFileSize,
            Int64 TotalBytesTransferred,
            Int64 StreamSize,
            Int64 StreamBytesTransferred,
            Int32 StreamNumber,
            Int32 CallbackReason,
            IntPtr SourceHandle,
            IntPtr DestinationHandle,
            Object UserData);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CopyFileEx(
            string lpExistingFileName,
            string lpNewFileName,
            APICopyProgressRoutine lpProgressRoutine,
            IntPtr lpData,
            ref Int32 lpCancel,
            CopyFileOptions dwCopyFlags);

        struct CopyFileResult {
            public readonly bool ReturnValue;
            public readonly int LastError;
            public CopyFileResult(bool rslt, int err) {
                ReturnValue = rslt;
                LastError = err;
            }
        }

        static private CopyFileResult CopyFileInternal(
            string sourceFilename,
            string destinationFilename,
            CopyProgressDelegate progressHandler,
            IntPtr userData,
            CopyFileOptions copyOptions,
            CancellationToken cancelToken) {
            // On error, throw IOException with the value from Marshal.GetLastWin32Error
            CopyProgressDelegate handler = progressHandler;
            int cancelFlag = 0;
            APICopyProgressRoutine callback;
            if (handler == null) {
                callback = null;
            } else {
                callback = new APICopyProgressRoutine((tfSize, xferBytes, strmSize, strmXferBytes,
                  strmNum, cbReason, srcHandle, dstHandle, udata) => {
                    var args = new CopyProgressArgs(tfSize, xferBytes, strmSize, strmXferBytes,
                      strmNum, cbReason, srcHandle, dstHandle, udata);
                    handler(args);
                    return (Int32)args.Result;
                });
            }
            if (cancelToken.CanBeCanceled) {
                cancelToken.Register(() => { cancelFlag = 1; });
            }
            bool rslt = CopyFileEx(
                sourceFilename,
                destinationFilename,
                callback,
                userData,
                ref cancelFlag,
                copyOptions);
            int err = 0;
            if (!rslt) {
                err = Marshal.GetLastWin32Error();
            }
            return new CopyFileResult(rslt, err);
        }

        static public void CopyFile(
            string sourceFilename,
            string destinationFilename,
            CopyProgressDelegate progressHandler,
            IntPtr userData,
            CopyFileOptions copyOptions,
            CancellationToken cancelToken) {
            var rslt = CopyFileInternal(
                sourceFilename,
                destinationFilename,
                progressHandler,
                userData,
                copyOptions,
                cancelToken);
            if (!rslt.ReturnValue) {
                throw new IOException(string.Format("Error copying file. GetLastError returns {0}.",
                  rslt.LastError));
            }
        }

        static public void CopyFile(
            string sourceFilename,
            string destinationFilename,
            CopyProgressDelegate progressHandler,
            IntPtr userData,
            CopyFileOptions copyOptions) {
            CopyFile(sourceFilename, destinationFilename, progressHandler, userData, copyOptions,
              CancellationToken.None);
        }

        static public void CopyFile(
            string sourceFilename,
            string destinationFilename) {
            CopyFile(sourceFilename, destinationFilename, null, IntPtr.Zero, CopyFileOptions.None);
        }

        static public void CopyFileNoBuffering(
            string sourceFilename,
            string destinationFilename) {
            CopyFile(sourceFilename, destinationFilename, null, IntPtr.Zero, CopyFileOptions.NoBuffering);
        }

        private delegate CopyFileResult CopyFileInvoker(
            string sourceFilename,
            string destinationFilename,
            CopyProgressDelegate progressHandler,
            IntPtr userData,
            CopyFileOptions copyOptions,
            CancellationToken cancelToken);

        static public IAsyncResult BeginCopyFile(
            string sourceFilename,
            string destinationFilename,
            CopyProgressDelegate progressHandler,
            IntPtr userData,
            CopyFileOptions copyOptions,
            CancellationToken cancelToken,
            AsyncCallback callback = null) {
            var caller = new CopyFileInvoker(CopyFileInternal);
            return caller.BeginInvoke(
                sourceFilename,
                destinationFilename,
                progressHandler,
                userData,
                copyOptions,
                cancelToken,
                callback,
                null);
        }

        static public void EndCopyFile(IAsyncResult ar) {
            AsyncResult rslt = (AsyncResult)ar;
            var caller = (CopyFileInvoker)rslt.AsyncDelegate;
            var copyResult = caller.EndInvoke(ar);
            if (!copyResult.ReturnValue) {
                throw new IOException(string.Format("Error copying file. GetLastError returns {0}.",
                  copyResult.LastError));
            }
        }
    }
}