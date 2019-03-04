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

using System.Configuration;
namespace GTX
{
    public static class Constant
    {
        public enum Salutation
        {
            Mr, Mrs, Miss
        }

        public enum Gender
        {
            Male,
            Female
        }

        /// <summary>
        /// Entity Status
        /// </summary>
        public class EntityStatus
        {
            /*
             * All System Status would be defined by *[StatusName]*
             */
            /// <summary>
            /// Special status entry for Soft Deletion Status
            /// </summary>
            public const string Deleted = "*DELETED*";

            /// <summary>
            /// you can't delete this data, but you can modify the data
            /// </summary>
            public const string SystemLocked = "*LOCKED*";

            /// <summary>
            /// Expired - once you set this entity into expired states you can't modify the entity
            /// you can only delete or soft delete the entity
            /// </summary>
            public const string SystemExpired = "*EXPIRED*";

            /* 
             * REGISTER         - ACTIVATE   -  DEACTIVATE   - PUBLISH   : UNPUBLISH - APPROVAL REQUEST  - APPROVAL    :  REJECT    -  WAIT CONFIRM         - CONFIRMATION : CANCELLATION : EXPIRE
             * NEW_REGISTRATION - ACTIVE     -  INACTIVE     - PUBLISHED : INACTIVE  - PENDING           - APPROVED    :  REJECTED  -  W84_CONFIRMATION     - CONFIRMED    : CANCELED     : EXPIRED
             */

            /// <summary>
            /// Entity has just registered : New Registration
            /// </summary>
            public const string NewRegistration = "NEW_REGISTRATION";

            /// <summary>
            /// Entity is in in-active mode : In-Active
            /// </summary>
            public const string Inactived = "INACTIVE";

            public const string WaitingActivation = "W84_ACTIVATION";

            public const string WaitingDeactivation = "W84_DEACTIVATION";
            /// <summary>
            /// Entity is in Active mode : Active
            /// </summary>
            public const string Actived = "ACTIVE";

            public const string Published = "PUBLISHED";

            public const string Removed = "REMOVED";

            public const string Pending = "PENDING";

            public const string WaitingApproval = "W84_APPROVAL";

            public const string Approved = "APPROVED";

            public const string Rejected = "REJECTED";

            public const string WaitingConfirmation = "W84_CONFIRMATION";

            public const string Confirmed = "CONFIRMED";

            public const string Cancel = "CANCELED";

            public const string WaitingAReview = "W84_REVIEW";

            public const string Reviewed = "REVIEWED";

            public const string Expired = "EXPIRED";

            public const string Locked = "LOCKED";

            public const string Banned = "BANNED";

            public const string Available = "AVAILABLE";

            public const string Unavailable = "UNAVAILABLE";

            public const string NoStatus = "";

        }

        #region  Licence
        public const string ServiceStackLicence = @"3357-e1JlZjozMzU3LE5hbWU6S2FyeWEgSGFybW9uaSBJbmRvbmVzaWEsVHlwZTpJbmRpZSxIYXNoOkdvZjhtWmZjOEFhd3c1ZnAveWxseDBtNnRDTWZIR2pGdlJnMGZUdFM5WU1FaUJCZUtQWmhISVVQdzFydTB6NlpCcCtzTXlta1VqWDRkZ1FrNlJQWEwzRzQwZnFBc3N5SG10RWp6OEZxVEw3d1lXNzRYcGlSQWVvYWhmdmg2bXdZTGZJUDE4ZmVyNU1rK2R4OFVBSDBNd1JVK2hPVmFYL0k3Mm1BQ0JpbFFacz0sRXhwaXJ5OjIwMTctMDEtMjZ9";
        #endregion

        //public static string ReqContextKey = "OS:REQUEST_CONTEXT";
        //public static string ReqAuthKey = "OS:AUTHENTICATION_CONTEXT";
        //public static string ReqSessionKey = "OS:SESSION_CONTEXT";
        //public static string ReqResponseKey = "OS:RESPONSE_CONTEXT";
        //public static string ErrorLogTpl = "<Error><Type>{0}</Type><MethodName>{1}</MethodName><ContextId>{2}</ContextId><Message>{3}</Message><AppContextId>{4}</AppContextId><RequestContextId>{5}</RequestContextId></Error>";
        //public static string DebugLogTpl = "<Debug><Type>{0}</Type><MethodName>{1}</MethodName><ContextId>{2}</ContextId><Message>{3}</Message><AppContextId>{4}</AppContextId><RequestContextId>{5}</RequestContextId></Debug>";
        //public static string RequestLogTpl = "<Request><Url>{0}</Url><MethodName>{1}</MethodName><ContextId>{2}</ContextId><StatusCode>{3}</StatusCode></Request>";

        ////public class LogLevel
        ////{
        ////    public const string Fatal = "FATAL";

        ////    public const string Error = "ERROR";

        ////    public const string Trace = "TRACE";

        ////    public const string Debug = "DEBUG";

        ////    public const string Warn = "WARNING";

        ////    public const string Info = "INFO";
        ////}

        ////public struct Pagination
        ////{
        ////    public const int DefaultPerPage = 25;
        ////}

        ////public struct Text
        ////{
        ////    public const string Empty = "";

        ////    public const string Whitespace = " ";
        ////}



        ////public struct Type
        ////{

        ////    public struct Salutation
        ////    {
        ////        public const string Mr = "Mr. ";
        ////        public const string Mrs = "Mrs. ";
        ////        public const string Miss = "Ms. ";
        ////    }

        ////    public struct Gender
        ////    {
        ////        public const string Male = "Male";
        ////        public const string Female = "Female";
        ////    }

        ////    public struct RateModelName
        ////    {
        ////        public const string COMMISSION = "COMMISSION";
        ////        public const string MARKUP = "MARKUP";
        ////    }

        ////}

        //public struct RegularExpression
        //{
        //    public const string WebsiteValidator = @"/^(http?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/";
        //    public const string Alphanumeric1 = @"^[a-zA-Z0-9\-]*$";
        //    public const string Alphanumeric2 = @"^[a-zA-Z0-9\-_ ']*$";
        //    public const string Alphanumeric3 = @"^[a-zA-Z0-9\-_\(\) ']*$";
        //    public const string Integers = @"^[0-9]*$";
        //    public const string Numbers = @"^[0-9,\.]*$";
        //}

        //public struct Utility
        //{
        //    /// <summary>
        //    /// The alphanumeric
        //    /// </summary>
        //    public const string Alphanumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        //    /// <summary>
        //    /// The Numeric
        //    /// </summary>
        //    public const string Numeric = "1234567890+-.,";

        //    //public struct RegularExpression
        //    //{
        //    //    public const string WebsiteValidator = @"/^(http?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/";
        //    //}
        //}

        //public struct Web
        //{
        //    //public struct HttpMethod
        //    //{
        //    //    public const string HttpGet = "GET";

        //    //    public const string HttpPost = "POST";

        //    //    public const string HttpPut = "PUT";

        //    //    public const string HttpDelete = "DELETE";
        //    //}

        //    //public struct ContentType
        //    //{
        //    //    public const string Json = "application/json";
        //    //    public const string Xml  = "application/xml";
        //    //}

        //    //public struct RequestHeader
        //    //{
        //    //    // ReSharper disable once MemberHidesStaticFromOuterClass
        //    //    public const string ContentType = "content-type";
        //    //}

        //    ////public struct Api
        //    ////{
        //    ////    public const string Url = "{0}/api/{1}/{2}";

        //    ////    public const string QueryStringId = "/{0}";

        //    ////    public const string QueryStringParentChildName = "?parentName={0}&childName={1}";

        //    ////    public const string QueryStringParentChildId = "?parentId={0}&childId={1}";

        //    ////    public const string InventoryMasterHostName = "InventoryMasterHostname";

        //    ////    public const string HotelManagementHostName = "HotelManagementHostName";

        //    ////    public const string HotelRegistrationHostName = "HotelRegistrationHostname";

        //    ////    public const string MarketingManagementHostName = "MarketingManagementHostName";

        //    ////    public const string UserManagementHostName = "UserManagementHostName";

        //    ////    public const string ReservationHostname = "ReservationHostname";

        //    ////    public const string HotelInformationHostName = "HotelInformationHostName";

        //    ////    public const string FinanceManagementHostName = "FinanceManagementHostName";

        //    ////    public const string AuditTrailHostName = "AuditTrailHostName";

        //    ////}

        //}

        ////public struct Action
        ////{
        ////    public const string Add = "Add";

        ////    public const string Edit = "Edit";

        ////    public const string Delete = "Delete";

        ////    public const string ResetPassword = "ResetPassword";

        ////    public const string ChangePassword = "ChangePassword";

        ////    public const string View = "ShowProfile";
        ////}

        ////public struct Currency
        ////{
        ////    public struct IDR
        ////    {
        ////        public const int OldId = 26;
        ////        public const string Code = "IDR";
        ////        public const string Name = "Indonesian Rupiah";
        ////    }
        ////    public struct USD
        ////    {
        ////        public const int OldId = 751;
        ////        public const string Code = "USD";
        ////        public const string Name = "United States Dollar";
        ////    }
        ////}

        ////public struct RateModelName
        ////{
        ////    public const string COMMISSION = "COMMISSION";
        ////    public const string MARKUP = "MARKUP";
        ////}

        ////public struct DiscountType
        ////{
        ////    public const string PERCENTAGE = "PERCENTAGE";
        ////    public const string AMOUNT = "AMOUNT";
        ////}

        ////public struct ErrorMessage
        ////{
        ////    public const string NoResult = "There is no result.";

        ////    public const string EmptyRequest = "Cannot leave the request empty.";

        ////    public const string CancellationRestrict = "This reservation cannot be cancelled.";

        ////    public const string ModificationRestrict = "This reservation cannot be modified.";

        ////    public const string AvailabilityNotFound = "Availability not found.";

        ////    public const string ReservationNotFound = "Reservation not found.";

        ////    public const string InsufficientCreditBalance = "Insufficient credit balance.";

        ////    public struct RequiredField
        ////    {
        ////        public const string HotelId = "HotelId is required.";
        ////        public const string RoomId = "RoomId is required.";
        ////        public const string NumberOfRoom = "NumberOfRoom is required.";
        ////        public const string RateTypeId = "RateTypeId is required.";
        ////        public const string CheckInDate = "CheckInDate is required.";
        ////        public const string CheckOutDate = "CheckOutDate is required.";
        ////        public const string FurtherRequest = "FurtherRequest is required.";

        ////        public const string TitleId = "TitleId is required.";
        ////        public const string FirstName = "FirstName is required.";
        ////        public const string LastName = "LastName is required.";
        ////        public const string Address = "Address is required.";
        ////        public const string City = "City is required.";
        ////        public const string PostalCode = "PostalCode is required.";
        ////        public const string NationalityId = "NationalityId is required.";
        ////        public const string CountryIdOfResidence = "CountryIdOfResidence is required.";
        ////        public const string Email = "Email is required.";
        ////        public const string HomePhone = "HomePhone is required.";
        ////        public const string MobilePhone = "MobilePhone is required.";

        ////        public const string Guest = "Guest is required";
        ////        public const string GuestFirstName = "GuestFirstName is required";
        ////        public const string GuestTitle = "GuestTitle is required";
        ////    }

        ////    public struct InvalidField
        ////    {
        ////        public const string TitleId = "TitleId is invalid.";
        ////    }
        ////}


    }

}
