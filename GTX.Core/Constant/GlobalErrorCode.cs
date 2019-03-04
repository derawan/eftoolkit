/*
 **************************************************************
 * Author: Irfansjah
 * Email: irfansjah@gmail.com
 * Created: 07/14/2018
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 **************************************************************  
*/


namespace GTX
{
    /// <summary>
    /// Global Error Code Definitions
    /// </summary>
    public static class GlobalErrorCode
    {
        /// <summary>
        /// Default System Error Code
        /// </summary>
        public const int DefaultSystemError = 0x3B9AC9FF; // 999999999;

        /// <summary>
        /// Object Not Set Error Code
        /// </summary>
        public const int ObjectNotSet = 0x3B9AC9FE; // 999999998;

        /// <summary>
        /// Method/Function Parameter  Value Not Set
        /// </summary>
        public const int ParameterNotSet = 0x3B9AC9FD; // 999999997;

        /// <summary>
        /// Method/Function Parameter Validation Error / Not Valid
        /// </summary>
        public const int ParameterValidationException = 0x3B9AC9FC; // 999999996;

        /// <summary>
        /// Parameter (name)Not Defined
        /// </summary>
        public const int ParameterNotFoundException = 0x3B9AC9FB; // 999999995;

        /// <summary>
        /// Command Handler Not Found or Not Registered Yet
        /// </summary>
        public const int CommandHandlerNotFoundException = 0x35A4E8FF; // 899999999;

        /// <summary>
        /// Validation Handler Not Found or Not Registered Yet
        /// </summary>
        public const int ValidationHandlerNotFoundException = 0x35A4E8FE; // 899999998;

        //public const int ObjectDefinitionNotRegistered          = 999999990;

        /// <summary>
        /// Entity already exists within collection or dataset
        /// </summary>
        public const int EntityAlreadyExistsException = 0x2FAF07FF; // 799999999;

        /// <summary>
        /// Entity Not Found within a collection or dataset
        /// </summary>
        public const int EntityNotFoundException = 0x2FAF07FE; // 799999998;


        public const int LoginRequiredException = 0x23C345FF; // 599999999

        public const int InternalServerError = 0x23C345FE; // 599999998

        public const int NotFound = 0x23C345FD; // 599999997

        public const int MethodNotAllowed = 0x23C345FC; // 599999996

        public const int UnsupportedMediaType = 0x23C345FB;

        public const int BadRequest = 0x23C345FA;

        public const int Forbidden = 0x23C345F9;

        public const int LengthRequired = 0x23C345F8;

        public const int Conflict = 0x23C345F7;

        public const int TimeOut = 0x23C345F6;

        public const int Gone = 0x23C345F5;


        public const int InvalidToken = 0x2FAF07F6; // 799999990

        public const string InvalidCommandTypeMessage = "Invalid Command Type";
    }


}
