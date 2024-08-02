using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace MUDTEMPLATE.DBData
{
    public class DB
    {
            readonly IConfiguration _configuration;
            public DB(IConfiguration configuration)
            {
                _configuration = configuration;
                Set_Connect(configuration.GetConnectionString("DefaultConnection") ?? "");
                
            }
            private static string CONN_STRING = "";

            public static void Set_Connect_DB(string Key_Config)
            {
                CONN_STRING = Key_Config;
            }
            public static void Set_Connect(string _conn)
            {
                CONN_STRING = _conn;
            }

            public static string Get_Connect()
            {
                return CONN_STRING;
            }

        private static DataTable ExecuteSPReader(string StoredProcedure, string tableName, params DictionaryEntry[] ParamName)
        {

            SqlConnection Connection = new SqlConnection(CONN_STRING);
            SqlCommand comm = new SqlCommand(StoredProcedure);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            foreach (DictionaryEntry paramV in ParamName)
            {
                comm.Parameters.AddWithValue(paramV.Key.ToString(), paramV.Value);
            }
            SqlDataAdapter resultDA = new SqlDataAdapter();
            resultDA.SelectCommand = comm;
            resultDA.SelectCommand.Connection = Connection;
            /*Connection.Open();*/
            DataSet resultDS = new DataSet();
            try
            {
                Connection.Open();
                resultDA.Fill(resultDS, tableName);
            }
            catch (Exception ex)
            {
                // Xử lý exception ở đây, ví dụ:
                Console.WriteLine($"Lỗi khi mở kết nối: {ex.Message}");
                return null; // hoặc xử lý theo nhu cầu của bạn
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            /*try
            {
                resultDA.Fill(resultDS, tableName);
            }
            catch { return null; }
            finally
            {
                Connection.Close();
            }*/
            return resultDS.Tables[0];
        }
        private static DataTable ExecuteSPReaderServer(string StoredProcedure, string tableName, string ServerConnection, params DictionaryEntry[] ParamName)
        {

            SqlConnection Connection = new SqlConnection(ServerConnection);
            SqlCommand comm = new SqlCommand(StoredProcedure);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            foreach (DictionaryEntry paramV in ParamName)
            {
                comm.Parameters.AddWithValue(paramV.Key.ToString(), paramV.Value);
            }
            SqlDataAdapter resultDA = new SqlDataAdapter();
            resultDA.SelectCommand = comm;
            resultDA.SelectCommand.Connection = Connection;
            Connection.Open();
            DataSet resultDS = new DataSet();
            try
            {
                resultDA.Fill(resultDS, tableName);
            }
            catch { return null; }
            finally
            {
                Connection.Close();
            }
            return resultDS.Tables[0];
        }
        private static bool ExecuteSPReaderReturnBool(string StoredProcedure, params DictionaryEntry[] ParamName)
        {
            bool isok = false;
            SqlConnection Connection = new SqlConnection(CONN_STRING);
            SqlCommand comm = new SqlCommand(StoredProcedure);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            foreach (DictionaryEntry paramV in ParamName)
            {
                comm.Parameters.AddWithValue(paramV.Key.ToString(), paramV.Value);
            }
            SqlDataAdapter resultDA = new SqlDataAdapter();
            resultDA.SelectCommand = comm;
            resultDA.SelectCommand.Connection = Connection;
            Connection.Open();
            try
            {
                comm.ExecuteNonQuery();
                isok = true;
            }
            catch (Exception ex)
            {
                isok = false;
            }
            finally
            {
                Connection.Close();
            }
            return isok;
        }

        private static object ExecuteSPReaderReturnIntValue(string StoredProcedure, string ReturnParam, params DictionaryEntry[] ParamName)
        {
            bool isok = false;
            SqlConnection Connection = new SqlConnection(CONN_STRING);
            SqlCommand comm = new SqlCommand(StoredProcedure);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            foreach (DictionaryEntry paramV in ParamName)
            {
                comm.Parameters.AddWithValue(paramV.Key.ToString(), paramV.Value);
            }
            SqlParameter RuturnValue = new SqlParameter(ReturnParam, SqlDbType.Int);
            RuturnValue.Direction = ParameterDirection.Output;
            comm.Parameters.Add(RuturnValue);

            SqlDataAdapter resultDA = new SqlDataAdapter();
            resultDA.SelectCommand = comm;
            resultDA.SelectCommand.Connection = Connection;
            Connection.Open();
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }
            return RuturnValue.Value;
        }

        private static object ExecuteSPReaderReturnStringValue(string StoredProcedure, string ReturnParam, params DictionaryEntry[] ParamName)
        {
            bool isok = false;
            SqlConnection Connection = new SqlConnection(CONN_STRING);
            SqlCommand comm = new SqlCommand(StoredProcedure);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            foreach (DictionaryEntry paramV in ParamName)
            {
                comm.Parameters.AddWithValue(paramV.Key.ToString(), paramV.Value);
            }
            SqlParameter RuturnValue = new SqlParameter(ReturnParam, SqlDbType.NVarChar);
            RuturnValue.Direction = ParameterDirection.Output;
            comm.Parameters.Add(RuturnValue);

            SqlDataAdapter resultDA = new SqlDataAdapter();
            resultDA.SelectCommand = comm;
            resultDA.SelectCommand.Connection = Connection;
            Connection.Open();
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }
            return RuturnValue.Value;
        }

        public static Boolean ExecuteQuery(string Query)
        {

            SqlConnection con = new SqlConnection(CONN_STRING);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 0;
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = Query;
            try
            {
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch { con.Close(); return false; }

        }
        #region Get data
        public static DataTable GetMatHangByBarcode(string maSieuThi, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            //parameters.Add(new SqlParameter("@ZoneId", SqlDbType.NVarChar), ZoneId);
            parameters.Add(new SqlParameter("@maSieuThi", SqlDbType.VarChar), maSieuThi);
            parameters.Add(new SqlParameter("@maDonVi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetMatHangByBarcode", "DATA", myArr);
        }
        public static DataTable GetMatHangByBarcodeLike(string maSieuThi, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            //parameters.Add(new SqlParameter("@ZoneId", SqlDbType.NVarChar), ZoneId);
            parameters.Add(new SqlParameter("@maSieuThi", SqlDbType.VarChar), maSieuThi);
            parameters.Add(new SqlParameter("@maDonVi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetMatHangByBarcodeLike", "DATA", myArr);
        }
        public static DataTable GetGiabanByMasieuthi(string maSieuThi, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maSieuThi", SqlDbType.VarChar), maSieuThi);
            parameters.Add(new SqlParameter("@maDonVi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetGiabanByMasieuthi", "DATA", myArr);
        }
        public static DataTable GetBoHangByBarcode(string maSieuThi)
        {
            ListDictionary parameters = new ListDictionary();
            //parameters.Add(new SqlParameter("@ZoneId", SqlDbType.NVarChar), ZoneId);
            parameters.Add(new SqlParameter("@maSieuThi", SqlDbType.VarChar), maSieuThi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetBoHangByBarcode", "DATA", myArr);
        }
        public static DataTable GetBoHangByBarcodeLike(string maSieuThi)
        {
            ListDictionary parameters = new ListDictionary();
            //parameters.Add(new SqlParameter("@ZoneId", SqlDbType.NVarChar), ZoneId);
            parameters.Add(new SqlParameter("@maSieuThi", SqlDbType.VarChar), maSieuThi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetBoHangByBarcodeLike", "DATA", myArr);
        }
        public static DataTable GetMatHangBoHang(string maBohang, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maBohang", SqlDbType.VarChar), maBohang);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetMatHangBoHang", "DATA", myArr);
        }

        public static DataTable GetMatHangBoHangKhohang(string maBohang, string maDonVi, string makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maBohang", SqlDbType.VarChar), maBohang);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            parameters.Add(new SqlParameter("@maKhohang", SqlDbType.VarChar), makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetMatHangBoHangKhohang", "DATA", myArr);
        }
        public static DataTable GetSxDmcongthucsxct(string Mactpk, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mactpk", SqlDbType.VarChar), Mactpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_SxDmcongthucsxct", "DATA", myArr);
        }

        public static DataTable SxDmcongthucsxctByPLV(string Mactpk, string Maplv, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mactpk", SqlDbType.VarChar), Mactpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            parameters.Add(new SqlParameter("@Maplv", SqlDbType.VarChar), Maplv);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_SxDmcongthucsxctByPLV", "DATA", myArr);
        }

        public static DataTable GetBohangchitietExtend(string maBohang, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maBohang", SqlDbType.VarChar), maBohang);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetBohangchitietExtend", "DATA", myArr);
        }
        public static DataTable GetAllBohangchitietExtendByMadonvi(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetAllBohangchitietExtendByMadonvi", "DATA", myArr);
        }
        public static DataTable GetAllMatHangBoHang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetAllMatHangBoHang", "DATA", myArr);
        }

        public static DataTable GetAllMatHangBoHangtheokho(string maDonVi, string makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            parameters.Add(new SqlParameter("@maKhohang", SqlDbType.VarChar), makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetAllMatHangBoHangtheokho", "DATA", myArr);
        }

        public static DataTable GetSumBoHang(string maBohang, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maBohang", SqlDbType.VarChar), maBohang);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetSumBoHang", "DATA", myArr);
        }
        public static DataTable GetMatHangBoHangNgayHienTai(string maDonVi, DateTime ngayPhatSinh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            parameters.Add(new SqlParameter("@ngayPhatSinh", SqlDbType.DateTime), ngayPhatSinh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetMatHangBoHangNgayHienTai", "DATA", myArr);
        }

        public static DataTable GetMatHangBoHangNgayHienTaitheokhohang(string maDonVi, DateTime ngayPhatSinh, string makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            parameters.Add(new SqlParameter("@maKhohang", SqlDbType.VarChar), makhohang);
            parameters.Add(new SqlParameter("@ngayPhatSinh", SqlDbType.DateTime), ngayPhatSinh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetMatHangBoHangNgayHienTaitheokhohang", "DATA", myArr);
        }

        public static DataTable GetSumBoHangAll(string maBohang, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maBohang", SqlDbType.VarChar), maBohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            //parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetSumBoHangFull", "DATA", myArr);
        }

        public static DataTable GetdataKhuyenmaict(string maChuongtrinhkm, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Machuongtrinh", SqlDbType.VarChar), maChuongtrinhkm);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            //parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Khuyenmaict", "DATA", myArr);
        }

        public static DataTable GetMathangdayweb(string TABLEXNT_NAME, string DB_NAME, string DB_NAME_XNT, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetMathangdayweb", "DATA", myArr);
        }

        public static DataTable GetLichLamViecNhanVien(string Madonvi, int Trangthaisudung, DateTime Ngaydauthang, DateTime Ngaycuoithang)
        {
            ListDictionary parameters = new ListDictionary();
            //parameters.Add(new SqlParameter("@ZoneId", SqlDbType.NVarChar), ZoneId);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            parameters.Add(new SqlParameter("@Ngaydauthang", SqlDbType.DateTime), Ngaydauthang);
            parameters.Add(new SqlParameter("@Ngaycuoithang", SqlDbType.DateTime), Ngaycuoithang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetLichLamViecNhanVien", "DATA", myArr);
        }

        public static DataTable GetLichlamvieccasangcachieu(string Madonvi, string Mabophan)
        {
            ListDictionary parameters = new ListDictionary();
            //parameters.Add(new SqlParameter("@ZoneId", SqlDbType.NVarChar), ZoneId);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mabophan", SqlDbType.VarChar), Mabophan);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetLichlamvieccasangcachieu", "DATA", myArr);
        }
        public static DataTable Getbaohiemnhanvien(string Madonvi, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getbaohiemnhanvien", "DATA", myArr);

        }

        public static DataTable GetDangKyNghiNhanVien(string Madonvi, DateTime Ngaydangky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Ngaydangky", SqlDbType.DateTime), Ngaydangky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDangKyNghiNhanVien", "DATA", myArr);

        }
        public static DataTable Getphucapnhanvien(string Madonvi, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getphucapnhanvien", "DATA", myArr);
        }
        public static DataTable GetDangKyLamThemNhanVien(string Madonvi, DateTime Ngaydauthang, DateTime Ngaycuoithang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Ngaydauthang", SqlDbType.DateTime), Ngaydauthang);
            parameters.Add(new SqlParameter("@Ngaycuoithang", SqlDbType.DateTime), Ngaycuoithang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDangKyLamThemNhanVien", "DATA", myArr);

        }

        public static DataTable GetLuongphucapnhanvien(string Madonvi, string Mabophan, int Trangthaisudung, DateTime Tungay, DateTime Denngay, int Binhthuong)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mabophan", SqlDbType.VarChar), Mabophan);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Binhthuong", SqlDbType.Int), Binhthuong);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetLuongphucapnhanvien", "DATA", myArr);

        }

        public static DataTable GetLuongtrachnhiemgiamsatnhanvien(string Madonvi, string Mabophan, int Trangthaisudung, int Trachnhiem, int Giamsat)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mabophan", SqlDbType.VarChar), Mabophan);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            parameters.Add(new SqlParameter("@Trachnhiem", SqlDbType.Int), Trachnhiem);
            parameters.Add(new SqlParameter("@Giamsat", SqlDbType.Int), Giamsat);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetLuongtrachnhiemgiamsatnhanvien", "DATA", myArr);

        }

        public static DataTable GetLuongkhenthuongkyluatnhanvien(string Madonvi, string Mabophan, int Loai, DateTime Tungay, DateTime Denngay, int Trangthaisudung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mabophan", SqlDbType.VarChar), Mabophan);
            parameters.Add(new SqlParameter("@Loai", SqlDbType.Int), Loai);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetLuongkhenthuongkyluatnhanvien", "DATA", myArr);

        }

        public static DataTable GetLuongluonghopdongnhanvien(string Madonvi, string Mabophan, int Trangthaisudung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mabophan", SqlDbType.VarChar), Mabophan);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetLuongluonghopdongnhanvien", "DATA", myArr);

        }

        public static DataTable GetLuongluonghopdongnhanvienNB(string Madonvi, string Manhanvien, int Trangthaisudung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetLuongluonghopdongnhanvienNB", "DATA", myArr);

        }


        public static DataTable GetTinhbangluongnhanvien(string Madonvi, string Mabophan, int Thang, int Nam, int Trangthaisudung, int Trangthai, int Khenthuong, int Kyluat, DateTime Tungay, DateTime Denngay, int Binhthuong)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mabophan", SqlDbType.VarChar), Mabophan);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.Int), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Khenthuong", SqlDbType.Int), Khenthuong);
            parameters.Add(new SqlParameter("@Kyluat", SqlDbType.Int), Kyluat);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Binhthuong", SqlDbType.Int), Binhthuong);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetTinhbangluongnhanvien", "DATA", myArr);

        }


        public static DataTable GetTinhbangluongnhanvienNB(string Madonvi, string Mabophan, int Thang, int Nam, int Trangthaisudung, int Trangthai, int Khenthuong, int Kyluat, DateTime Tungay, DateTime Denngay, int Binhthuong)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mabophan", SqlDbType.VarChar), Mabophan);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.Int), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Khenthuong", SqlDbType.Int), Khenthuong);
            parameters.Add(new SqlParameter("@Kyluat", SqlDbType.Int), Kyluat);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Binhthuong", SqlDbType.Int), Binhthuong);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetTinhbangluongnhanvienNB", "DATA", myArr);

        }


        public static DataTable GetLuonghopdongtheobophan(string Madonvi, string Mabophan)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mabophan", SqlDbType.VarChar), Mabophan);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetLuonghopdongtheobophan", "DATA", myArr);

        }

        public static DataTable GetdataMathangcandientu(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetdataMathangcandientu", "DATA", myArr);

        }

        public static DataTable GetLuongluonghopdongnhanvienPhongban(string Madonvi, string Maphongban, int Trangthaisudung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maphongban", SqlDbType.VarChar), Maphongban);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetLuongluonghopdongnhanvienPhongban", "DATA", myArr);

        }
        public static DataTable GetDkchuyenbophan(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDkchuyenbophan", "DATA", myArr);

        }

        public static DataTable Getlichlamviecthangnhanvien(string Madonvi, int Thang, int Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.Int), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getlichlamviecthangnhanvien", "DATA", myArr);

        }
        public static DataTable GetDataDathangctByListMagiaodichpk(string ListMagiaodichpk, string Madonvi, string TABLEXNT_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@ListMagiaodichpk", SqlDbType.VarChar), ListMagiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataDathangctByListMagiaodichpk", "DATA", myArr);
        }
        public static DataTable GetDataNCCByListMagiaodichpk(string ListMagiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@ListMagiaodichpk", SqlDbType.VarChar), ListMagiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataNCCByListMagiaodichpk", "DATA", myArr);
        }
        public static DataTable GetDataKehoachctByListMagiaodichpk(string ListMagiaodichpk, string Madonvi, string TABLEXNT_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@ListMagiaodichpk", SqlDbType.VarChar), ListMagiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataKehoachctByListMagiaodichpk", "DATA", myArr);
        }
        public static DataTable GetData_VTHH_Mathangshowweb(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Mathangshowweb", "DATA", myArr);
        }
        public static DataTable GetData_VTHH_DatHangTuDong(string Madonvi, string TABLEXNT_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_DatHangTuDong", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_DatHangTuDongCThuctheokho(string Madonvi, string Mancc, string Makhohang, DateTime tungay, DateTime denngay, string TABLEXNT_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mancc", SqlDbType.VarChar), Mancc);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), denngay);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_DatHangTuDongCThuctheokho", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_DatHangTuDongTong(string Madonvi, string TABLEXNT_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_DatHangTuDongTong", "DATA", myArr);
        }
        public static DataTable GetData_VTHH_DieuchuyenTuDong(string Madonvi, string MakhoXuat, string MakhoNhap, string TABLEXNT_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@MakhoXuat", SqlDbType.VarChar), MakhoXuat);
            parameters.Add(new SqlParameter("@MakhoNhap", SqlDbType.VarChar), MakhoNhap);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_DieuchuyenTuDong", "DATA", myArr);
        }
        public static DataTable GetData_KT_DanhMucTaiKhoanKetChuyenCT(string Matkketchuyen, string madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Matkketchuyen", SqlDbType.VarChar), Matkketchuyen);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_KT_DanhMucTaiKhoanKetChuyenCT", "DATA", myArr);

        }

        public static DataTable XNT_CHECKKIEMKE(string TABLEXNT_NAME, string DB_NAME_XNT, string DB_NAME, string Makhohang
            , string Manganh, string Manhomhang, string Makehang, string Masieuthi, string Madonvi, string Makhachhang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makehang", SqlDbType.VarChar), Makehang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_CHECKKIEMKE", "DATA", myArr);

        }

        public static DataTable Getdata_VTHH_Thongtinthanhtoan(string Madonvi, string Magiaodichpk, string Mactktpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Mactktpk", SqlDbType.VarChar), Mactktpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Thongtinthanhtoan", "DATA", myArr);

        }


        public static DataTable GetData_VT_GiaoDichThueXeCT(string Madonvi, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VT_GiaoDichThueXeCT", "DATA", myArr);

        }
        //Xuất file XML  nghiệp vụ - Kiểm kê. --------------------
        public static DataTable Getdata_NV_VTHH_Kiemke_XuatXML(string TABLEXNT_NAME, string DB_NAME_XNT, string DB_NAME, string Makhohang, string Manganh, string Manhomhang, string Makehang, string Masieuthi, string Madonvi, string Makhachhang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makehang", SqlDbType.VarChar), Makehang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_NV_VTHH_Kiemke_XuatXML", "DATA", myArr);

        }

        public static DataTable Getdata_VTHH_XuatXML_BoHang(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_XuatXML_BoHang", "DATA", myArr);

        }

        public static DataTable GetBohangamchietkhau(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetBohangamchietkhau", "DATA", myArr);

        }

        public static DataTable GetBohangbymasieuthi(string Madonvi, string masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maSieuthi", SqlDbType.VarChar), masieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetBohangbymasieuthi", "DATA", myArr);

        }

        public static DataTable Getdata_VTHH_XuatXML_BoHangct(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_XuatXML_BoHangct", "DATA", myArr);

        }
        // kết thúc xuất XML-------------------------------------------
        public static DataTable GetData_VTHH_GiaodichphuExit(string Madonvi, string Magiaodichphu, string TableName, string ColumnName)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichphu", SqlDbType.VarChar), Magiaodichphu);
            parameters.Add(new SqlParameter("@TableName", SqlDbType.VarChar), TableName);
            parameters.Add(new SqlParameter("@ColumnName", SqlDbType.VarChar), ColumnName);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_GiaodichphuExit", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_getHoTenNguoiDung(string tenDangnhap, string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tendangnhap", SqlDbType.VarChar), tenDangnhap);
            parameters.Add(new SqlParameter("@maDonVi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_getHoTenNguoiDung", "DATA", myArr);
        }

        // Lương hợp đồng theo nhân viên -----------------Name:Manhtran.
        public static DataTable Getdata_NS_LuongHopDongNhanVien(string Madonvi, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_NS_LuongHopDongNhanVien", "DATA", myArr);

        }


        public static DataTable Getdata_Nhanvienkhachhang(string Madonvi, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Nhanvienkhachhang", "DATA", myArr);

        }
        public static DataTable Getdata_VTHH_XulyAmCt(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho
            , string Manganhhang, string Manhomhang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_XulyAmCt", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_XulyAmTh(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho
            , string Manganhhang, string Manhomhang, string DB_NAME_XNT, string TABLEXNT_NAME, string Makhoam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makhoban", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Makhoam", SqlDbType.VarChar), Makhoam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_XulyAmTh", "DATA", myArr);
        }

        public static DataTable GetCanhBaoTon(string Madonvi, string TABLEXNT_NAME, string DB_NAME_XNT, string trangthaikd, string Makhohang, string Manganhhang, string Manhomhang, string Makhachhang, string sosanh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonVi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@trangthaikd", SqlDbType.VarChar), trangthaikd);

            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Sosanh", SqlDbType.VarChar), sosanh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetCanhBaoTon", "DATA", myArr);
        }


        public static DataTable GetCaiDatCanhBaoTon(string Madonvi, string Makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonVi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetCaiDatCanhBaoTon", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_Tinhthuongnv(DateTime Tungay, DateTime Denngay, string Madonvi, string Manhomptnx, string manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhomptnx", SqlDbType.VarChar), Manhomptnx);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@manhanvien", SqlDbType.VarChar), manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Tinhthuongnv", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichquaychitietbymagiaodich(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichquaychitietbymagiaodich", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichctdoitractquay(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichctdoitractquay", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichctdoitractgd(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichctdoitractgd", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_KhohangGia(string Madonvi, string Makhohang, string Maloaigia)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Maloaigia", SqlDbType.VarChar), Maloaigia);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KhohangGia", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_PhuongThucHachToanByKhohang(string Madonvi, string Makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_PhuongThucHachToanByKhohang", "DATA", myArr);
        }
        public static DataTable GetNguoidung(string TenNguoidung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TenNguoidung", SqlDbType.VarChar), TenNguoidung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetNguoidung", "DATA", myArr);
        }
        public static DataTable GetDonvi()
        {
            ListDictionary parameters = new ListDictionary();
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDonvi", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_DmptnxByKhohang(string Madonvi, string Tendangnhap)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tendangnhap", SqlDbType.VarChar), Tendangnhap);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_DmptnxByKhohang", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_DmChungtuByKhohang(string Madonvi, string Tendangnhap)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tendangnhap", SqlDbType.VarChar), Tendangnhap);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_DmChungtuByKhohang", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_KhohangGiaByMasieuthi(string Madonvi, string Masieuthi, string Makhohang, string Maloaigia)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Maloaigia", SqlDbType.VarChar), Maloaigia);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KhohangGiaByMasieuthi", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_KhohangGiaByMabo(string Madonvi, string Makhohang, string Mabohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Mabohang", SqlDbType.VarChar), Mabohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KhohangGiaByMabo", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_Bohangctkhohanggia(string Madonvi, string Makhohang, string Mabohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Mabohang", SqlDbType.VarChar), Mabohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Bohangctkhohanggia", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_KhohangGiaByNgay(string Madonvi, string Makhohang, string Maloaigia, DateTime Ngayapdung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Maloaigia", SqlDbType.VarChar), Maloaigia);
            parameters.Add(new SqlParameter("@Ngayapdung", SqlDbType.DateTime), Ngayapdung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KhohangGiaByNgay", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_Khohangbohanggia(string Madonvi, string Makhohang, string Mabohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Mabohang", SqlDbType.VarChar), Mabohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Khohangbohanggia", "DATA", myArr);
        }

        public static DataTable GetAllMathangkhohanggia(string Madonvi, string Makhohang, DateTime Ngayapdung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Ngayapdung", SqlDbType.DateTime), Ngayapdung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetAllMathangkhohanggia", "DATA", myArr);
        }

        public static DataTable GetAllMathangkhohanggiabyngayphatsinh(string Madonvi, string Makhohang, DateTime Ngayapdung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Ngayapdung", SqlDbType.DateTime), Ngayapdung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetAllMathangkhohanggiabyngayphatsinh", "DATA", myArr);
        }

        public static DataTable GetAllBoghangctkhohanggia(string Madonvi, string Makhohang, DateTime Ngayapdung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Ngayapdung", SqlDbType.DateTime), Ngayapdung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetAllBoghangctkhohanggia", "DATA", myArr);
        }

        public static DataTable GetAllBoghangctkhohanggiabyNgayphatsinh(string Madonvi, string Makhohang, DateTime Ngayapdung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Ngayapdung", SqlDbType.DateTime), Ngayapdung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetAllBoghangctkhohanggiabyNgayphatsinh", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_KMMOTSOSANPHAM(string Madonvi, DateTime ngayPS)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Ngayps", SqlDbType.DateTime), ngayPS);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_KMMOTSOSANPHAM", "DATA", myArr);

        }

        public static DataTable Getdata_VTHH_Phanquyennvhh(string Madonvi, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Phanquyennvhh", "DATA", myArr);

        }

        public static DataTable Getdata_VTHH_Phanquyennvhhnhomhang(string Madonvi, string Manhanvien, string Manganh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Phanquyennvhhnhomhang", "DATA", myArr);

        }

        public static DataTable GetData_VTHH_DSphanquyennvhh()
        {
            ListDictionary parameters = new ListDictionary();
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_DSphanquyennvhh", "DATA", myArr);

        }


        public static DataTable Getdata_VTHH_Phieuinxbnhieugd(string Madonvi, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Phieuinxbnhieugd", "DATA", myArr);

        }

        public static DataTable Getdata_VTHH_Tracuudonhangtheonv(DateTime Tungay, DateTime Denngay, string Madonvi, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Tracuudonhangtheonv", "DATA", myArr);

        }

        public static DataTable Getdata_VTHH_giaodichctkm(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_giaodichctkm", "DATA", myArr);
        }
        public static DataTable GetData_Sx_QuyctktByMagiaodichphu(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichphu", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_QuyctktByMagiaodichphu", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Mathangchuadongbo(string Madonvitong, string Madonvidongbo)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvitong", SqlDbType.VarChar), Madonvitong);
            parameters.Add(new SqlParameter("@Madonvidongbo", SqlDbType.VarChar), Madonvidongbo);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Mathangchuadongbo", "DATA", myArr);
        }
        public static DataTable Getdata_KT_MaxThangChotSo(string Madonvi, int Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_MaxThangChotSo", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_MaNgoaiVungKiemKe(string Madonvi, string Masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_MaNgoaiVungKiemKe", "DATA", myArr);

        }
        public static DataTable Getdata_VTHH_GiaodichPhanbo(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_GiaodichPhanbo", "DATA", myArr);
        }
        public static DataTable GetDataByMagiaodichDaPhanbo(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataByMagiaodichDaPhanbo", "DATA", myArr);
        }

        public static DataTable Getdata_LinkquysotienbyMagiaodich(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_LinkquysotienbyMagiaodich", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_Chitietkhachhangdongop(string Madonvi, string Madongop, string Masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), Madongop);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Chitietkhachhangdongop", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_Exportkhohanggia(string Madonvi, string Makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Exportkhohanggia", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_ExportkhohanggiaBieuDoc(string Madonvi, string Makhohang, string Mancc, string Manganh, string Manhomhang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Mancc", SqlDbType.VarChar), Mancc);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_ExportkhohanggiaBieuDoc", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_ExportKhohanggiaBieuNgang(string Madonvi, string Makhohang, string Mancc, string Manganh, string Manhomhang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Mancc", SqlDbType.VarChar), Mancc);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_ExportKhohanggiaBieuNgang", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_GetDinhmucDoidiem(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manganh, string Manhomhang
            , string Makhachhang, int Status)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Status", SqlDbType.Int), Status);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_GetDinhmucDoidiem", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_KhohangtichdiemAll(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KhohangtichdiemAll", "DATA", myArr);
        }


        public static DataTable getdata_VTHH_LICHDATHANGTHEOKHACHHANG(string Madonvi, string makhachhang, string makhohang, int thang, int nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachhang);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), makhohang);
            parameters.Add(new SqlParameter("@Month", SqlDbType.Int), thang);
            parameters.Add(new SqlParameter("@Year", SqlDbType.Int), nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("getdata_VTHH_LICHDATHANGTHEOKHACHHANG", "DATA", myArr);
        }
        public static DataTable getdata_VTHH_LICHDATHANGTHEOKHACHHANGTHEOTHU(string Madonvi, string makhachhang, string makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachhang);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("getdata_VTHH_LICHDATHANGTHEOKHACHHANGTHEOTHU", "DATA", myArr);
        }

        public static DataTable getdata_VTHH_LICHDATHANGKHOTHEOKHACHHANG(string Madonvi, string makhachhang, DateTime ngaydat)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachhang);
            parameters.Add(new SqlParameter("@ngaydat", SqlDbType.DateTime), ngaydat);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("getdata_VTHH_LICHDATHANGKHOTHEOKHACHHANG", "DATA", myArr);
        }

        public static DataTable getdata_VTHH_LICHDATHANGKHOTHEOKHACHHANGTHEOTHU(string Madonvi, string makhachhang, string thudat)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachhang);
            parameters.Add(new SqlParameter("@Thudat", SqlDbType.VarChar), thudat);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("getdata_VTHH_LICHDATHANGKHOTHEOKHACHHANGTHEOTHU", "DATA", myArr);
        }


        internal static DataTable Getdata_KT_Taikhoannganhang()
        {
            ListDictionary parameters = new ListDictionary();
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_Taikhoannganhang", "DATA", myArr);
        }

        public static DataTable Getdata_KT_QuyckktByTungayDenngay(string Madonvi, DateTime tungay, DateTime denngay, int trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denngay);
            parameters.Add(new SqlParameter("@trangthai", SqlDbType.Int), trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_QuyckktByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Getdata_KT_QuyckktctByTungayDenngay(string Madonvi, DateTime tungay, DateTime denngay, int trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denngay);
            parameters.Add(new SqlParameter("@trangthai", SqlDbType.Int), trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_QuyckktctByTungayDenngay", "DATA", myArr);
        }

        public static DataTable GetData_CHECKTANGGIAMGIANHAP(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_CHECKTANGGIAMGIANHAP", "DATA", myArr);

        }

        public static DataTable GetData_CHECKTANGGIAMGIANHAPDALUUTHUC(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_CHECKTANGGIAMGIANHAPDALUUTHUC", "DATA", myArr);

        }

        public static DataTable GetData_Diemconlaicuakhachhang(string Madonvi, string Makhachhang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Diemconlaicuakhachhang", "DATA", myArr);

        }

        #endregion

        #region getdata_hethong
        public static DataTable GetData_HT_NguoidungKhohangByTendangnhap(string madonvi, string tendangnhap)
        {
            ListDictionary parameters = new ListDictionary();
            //parameters.Add(new SqlParameter("@ZoneId", SqlDbType.NVarChar), ZoneId);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            parameters.Add(new SqlParameter("@Tendangnhap", SqlDbType.VarChar), tendangnhap);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_HT_NguoidungKhohangByTendangnhap", "DATA", myArr);
        }
        #endregion

        #region Get Data Sản xuất
        public static DataTable GetData_SX_SxDmcongthucsxct(string Madonvi, string Masieuthi, decimal Soluong, int status, string Magiaodichpk, string Mactpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Soluong", SqlDbType.Decimal), Soluong);
            parameters.Add(new SqlParameter("@Status", SqlDbType.Int), status);
            parameters.Add(new SqlParameter("@Mactpk", SqlDbType.VarChar), Mactpk);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_SX_SxDmcongthucsxct", "DATA", myArr);
        }
        #endregion

        #region Các stored liên quan đến giao dịch
        public static DataTable GetDataGiaodichctByMagiaodichpkFromServer(string Magiaodichpk, string Madonvi, string ServerSqlConnection)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderServer("GetDataGiaodichctByMagiaodichpkDongboBanle", "DATA", ServerSqlConnection, myArr);
        }
        public static DataTable GetDataGiaodichctByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichctByMagiaodichpk", "DATA", myArr);
        }
        public static DataTable GetData_GiabanBuonGanNhat(string strMakhachhang, string Madonvi, string strMasieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), strMakhachhang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), strMasieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GiabanBuonGanNhat", "DATA", myArr);
        }
        public static DataTable GetDataInTemByListMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@ListMagiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataInTemByListMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichctQCByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichctQCByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichctHaiquanByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichctHaiquanByMagiaodichpk", "DATA", myArr);
        }
        public static DataTable GetDataGiaodichctmauByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichctmauByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetDataGDcthangbanByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGDcthangbanByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetDataGDcthangkmByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGDcthangkmByMagiaodichpk", "DATA", myArr);
        }
        public static DataTable GetData_VTHH_GiaodichNhapMua_Phanbo(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_GiaodichNhapMua_Phanbo", "DATA", myArr);
        }
        public static DataTable GetDataCoppyGDCTbyMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataCoppyGDCTbyMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichTondauctct(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichTondauctct", "DATA", myArr);
        }
        //khach hàng giá
        public static DataTable Getdata_VTHH_KhachhangGia(string Makhachhang, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KhanghangGia", "DATA", myArr);
        }
        //khách hàng giá theo nhóm
        public static DataTable Getdata_VTHH_KhachhangGiabyNhom(string Makhachhang, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KhanghangGiabyNhom", "DATA", myArr);
        }
        //khách hàng giá theo ngành
        public static DataTable Getdata_VTHH_KhachhangGiabyNganh(string Makhachhang, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KhanghangGiabyNganh", "DATA", myArr);
        }

        //khách hàng giá by key
        public static DataTable Getdata_VTHH_KhanghangGiabyKey(string Makhachhang, string Madonvi, int maloaigia, string maapdung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maloaigia", SqlDbType.Int), maloaigia);
            parameters.Add(new SqlParameter("@Maapdung", SqlDbType.VarChar), maapdung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KhanghangGiabyKey", "DATA", myArr);
        }

        //nhóm khach hàng giá
        public static DataTable Getdata_VTHH_NhomkhanghangGia(string Manhomkh, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Manhomkhachhang", SqlDbType.VarChar), Manhomkh);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_NhomkhanghangGia", "DATA", myArr);
        }
        //nhóm khách hàng giá theo nhóm
        public static DataTable Getdata_VTHH_NhomKhanghangGiabyNhom(string maNhomkh, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Manhomkhachhang", SqlDbType.VarChar), maNhomkh);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_NhomKhanghangGiabyNhom", "DATA", myArr);
        }
        //nhóm khách hàng giá theo ngành
        public static DataTable Getdata_VTHH_NhomKhanghangGiabyNganh(string maNhomkh, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Manhomkhachhang", SqlDbType.VarChar), maNhomkh);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_NhomKhanghangGiabyNganh", "DATA", myArr);
        }
        //Nhóm khách hàng giá theo key

        public static DataTable Getdata_VTHH_NhomkhanghangGiabyKey(string Manhomkhachhang, string Madonvi, int maloaigia, string maapdung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Manhomkhachhang", SqlDbType.VarChar), Manhomkhachhang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maloaigia", SqlDbType.Int), maloaigia);
            parameters.Add(new SqlParameter("@Maapdung", SqlDbType.VarChar), maapdung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_NhomkhanghangGiabyKey", "DATA", myArr);
        }
        //Nhân viên giá
        public static DataTable Getdata_VTHH_NhanvienGia(string maNhanvien, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), maNhanvien);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_NhanvienGia", "DATA", myArr);
        }
        //Nhân viên giá theo nhóm
        public static DataTable Getdata_VTHH_NhanvienGiabyNhom(string maNhanvien, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), maNhanvien);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_NhanvienGiabyNhom", "DATA", myArr);
        }
        //Nhân viên giá theo ngành
        public static DataTable Getdata_VTHH_NhanvienGiabyNganh(string maNhanvien, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), maNhanvien);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_NhanvienGiabyNganh", "DATA", myArr);
        }
        //Nhân viên giá theo key
        public static DataTable Getdata_VTHH_NhanvienGiabyKey(string maNhanvien, string Madonvi, int maloaigia, string maapdung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), maNhanvien);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maloaigia", SqlDbType.Int), maloaigia);
            parameters.Add(new SqlParameter("@Maapdung", SqlDbType.VarChar), maapdung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_NhanvienGiabyKey", "DATA", myArr);
        }
        public static DataTable GetData_VTHH_GDCTKhongtkbymagdpk(string Magiaodichpk, string Madonvi, string manhomptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhomptnx", SqlDbType.VarChar), manhomptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_GDCTKhongtkbymagdpk", "DATA", myArr);
        }

        public static DataTable GetDataSerialByMagiaodichpkAndMasieuthi(string Magiaodichpk, string Madonvi, string Masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataSerialByMagiaodichpkAndMasieuthi", "DATA", myArr);
        }

        public static DataTable GetDataMathangSerialByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataMathangSerialByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetDataSerialByMasieuthiAndTrangthai(string Madonvi, string Masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataSerialByMasieuthiAndTrangthai", "DATA", myArr);
        }

        public static DataTable GetDataSerialXuatByMasieuthi(string Madonvi, string Masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataSerialXuatByMasieuthi", "DATA", myArr);
        }

        public static DataTable GetDataAllSerial(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataAllSerial", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichNXKiemkeByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichNXKiemkeByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichChuyenKhoByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichChuyenKhoByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichDenghiXuatHangByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichDenghiXuatHangByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichByTungayDenngayFromSerVer(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay
            , string Makhachhang, string ServerSqlConnection)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderServer("Fill_GiaodichByTungayDenngayFromServer", "DATA", ServerSqlConnection, myArr);
        }
        public static DataTable Fill_GiaodichNhapKhauByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichNhapKhauByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichKhohangByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichKhohangByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichNTLAIByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichNTLAIByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichDichvuByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichDichvuByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichmauByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichmauByTungayDenngay", "DATA", myArr);
        }

        public static DataTable Fill_GiaodichSanXuatByTungayDenngay(string LoaiChungtu, string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            parameters.Add(new SqlParameter("@LoaiChungtu", SqlDbType.VarChar), LoaiChungtu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichSanXuatByTungayDenngay", "DATA", myArr);
        }
        public static DataTable GetData_VTHH_SinhNhatKhachhang(string Madonvi, DateTime tuNgay, DateTime denNgay, string listNgaythang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            parameters.Add(new SqlParameter("@listNgaythang", SqlDbType.VarChar), listNgaythang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_SinhNhatKhachhang", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichVinamilkByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichVinamilkByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_DathangByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_DathangByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_KehoachDathangByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_KehoachDathangByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichquayByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay, string MaKhoban)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), MaKhoban);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichquayByTungayDenngay", "DATA", myArr);
        }

        public static DataTable Fill_GiaodichquayF3ByTungayDenngay(string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay, string MaKhoban)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), MaKhoban);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichquayF3ByTungayDenngay", "DATA", myArr);
        }

        public static DataTable Fill_GiaodichquaydodangByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay, string MaKhoban)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), MaKhoban);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichquaydodangByTungayDenngay", "DATA", myArr);
        }

        public static DataTable Fill_Giaodichquay_Dinhmuc_ByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_Giaodichquay_Dinhmuc_ByTungayDenngay", "DATA", myArr);
        }

        public static DataTable Fill_Giaodichquay_NguyenlieuByMagiaodichpk(string Magiaodichquays, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichquay", SqlDbType.VarChar), Magiaodichquays);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_Giaodichquay_NguyenlieuByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable Fill_GiaodichquayBySerial(string Trangthai, string Madonvi, string maNhomptnx, string tuNgay, string denNgay, string Serial)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@Serial", SqlDbType.VarChar), Serial);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.VarChar), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.VarChar), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichquayBySerial", "DATA", myArr);
        }
        public static DataTable GetDataGiaodichquayctByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichquayctByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichquayf3ctByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichquayf3ctByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichquaydodangctByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichquaydodangctByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichquayctInA4ByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichquayctInA4ByMagiaodichpk", "DATA", myArr);
        }
        public static DataTable GetDataGiaodichquayctByMagiaodichpkTralai(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichquayctByMagiaodichpkTralai", "DATA", myArr);
        }
        public static DataTable GetDataLichsuByTungayDenngay(string Madonvi, string maNhomptnx, string tuNgay, string denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.VarChar), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.VarChar), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataLichsuByTungayDenngay", "DATA", myArr);
        }
        public static DataTable GetDataMasieuthiLikeBarcode(string Madonvi, string Barcode)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Barcode", SqlDbType.VarChar), Barcode);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataMasieuthiLikeBarcode", "DATA", myArr);
        }
        public static DataTable Getgiaodichchuathanhtoan(string makhachang, string madonvi, bool trangthai, int tichchat)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            parameters.Add(new SqlParameter("@Tinhchat", SqlDbType.Int), tichchat);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Bit), trangthai);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getgiaodichchuathanhtoan", "DATA", myArr);
        }

        public static DataTable GetCtucongno(string makhachang, string madonvi, int tichchat, string matk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            parameters.Add(new SqlParameter("@Tinhchat", SqlDbType.Int), tichchat);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), matk);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetCtucongno", "DATA", myArr);
        }
        public static DataTable GetCongnoChuathanhtoan(string makhachang, string madonvi, bool trangthai, int tichchat, string Mangoaite)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            parameters.Add(new SqlParameter("@Mangoaite", SqlDbType.VarChar), Mangoaite);
            parameters.Add(new SqlParameter("@Tinhchat", SqlDbType.Int), tichchat);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetCongnoChuathanhtoan", "DATA", myArr);
        }

        public static DataTable GetCongnoChuathanhtoanNCC(string makhachang, string madonvi, string Manhomptnx, string Mangoaite, DateTime tungay, DateTime denNgay, DateTime ngaybatdauHT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            parameters.Add(new SqlParameter("@Mangoaite", SqlDbType.VarChar), Mangoaite);
            parameters.Add(new SqlParameter("@Manhomptnx", SqlDbType.VarChar), Manhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            parameters.Add(new SqlParameter("@ngaybatdauHT", SqlDbType.DateTime), ngaybatdauHT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetCongnoChuathanhtoanNCC", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_GiaodichchuaTTbymaGDphu(string Magiaodich, string madonvi, bool trangthai, int tichchat)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), Magiaodich);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            parameters.Add(new SqlParameter("@Tinhchat", SqlDbType.Int), tichchat);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Bit), trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_GiaodichchuaTTbymaGDphu", "DATA", myArr);
        }

        public static DataTable GetTongTienPhaiTTByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetTongTienPhaiTTByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetAllGiaodichthanhtoan(string Madonvi, int tinhchat, DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tinhchat", SqlDbType.Int), tinhchat);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.VarChar), tungay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.VarChar), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetAllGiaodichthanhtoan", "DATA", myArr);
        }
        public static DataTable GetData_VTHH_KiemkectByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_KiemkectByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetData_VT_BaoDuongXe(string Maxe, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Maxe", SqlDbType.VarChar), Maxe);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VT_BaoDuongXe", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Giaodichgopdon(string Madonvi, string Matuyen, string Magiaodich, string magiaodichorderby)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Matuyen", SqlDbType.VarChar), Matuyen);
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), Magiaodich);
            parameters.Add(new SqlParameter("@Magiaodichorderby", SqlDbType.VarChar), magiaodichorderby);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Giaodichgopdon", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Giaodichchuagopdon(string Madonvi, string Matuyen, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Matuyen", SqlDbType.VarChar), Matuyen);
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Giaodichchuagopdon", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_DSGiaodichgopdon(string Madonvi, DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TuNgay", SqlDbType.VarChar), tungay);
            parameters.Add(new SqlParameter("@DenNgay", SqlDbType.VarChar), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_DSGiaodichgopdon", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_DSGiaodichGiaonhan(string Madonvi, DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TuNgay", SqlDbType.VarChar), tungay);
            parameters.Add(new SqlParameter("@DenNgay", SqlDbType.VarChar), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_DSGiaodichGiaonhan", "DATA", myArr);
        }

        public static DataTable Fill_GiaodichALLByTungayDenngay(string Trangthai, string Madonvi, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichALLByTungayDenngay", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Giaodichgiaonhan(string Madonvi, string magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Giaodichgiaonhan", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Dongopbymagiaodichphu(string Madonvi, string magiaodich, string magiaodichorderby)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), magiaodich);
            parameters.Add(new SqlParameter("@Magiaodichorderby", SqlDbType.VarChar), magiaodichorderby);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Dongopbymagiaodichphu", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Phieuindongop(string Madonvi, string madongop)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Phieuindongop", "DATA", myArr);
        }
        public static DataTable GetData_VTHH_Manganhtheodongop(string Madonvi, string madongop)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Manganhtheodongop", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Phieuindongoptheonganh(string Madonvi, string madongop, string manganh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), manganh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Phieuindongoptheonganh", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Phieuindongopbanbuon(string Madonvi, string madongop)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Phieuindongopbanbuon", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_PhieuindongoptheoKHbanbuon(string Madonvi, string madongop, string maorder)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            parameters.Add(new SqlParameter("@Magiaodichorderby", SqlDbType.VarChar), maorder);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_PhieuindongoptheoKHbanbuon", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_Lichsudonhang(int trangthai, string makhachhang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), trangthai);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachhang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Lichsudonhang", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_PhieuindongoptheoKH(string Madonvi, string madongop, string maorder)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            parameters.Add(new SqlParameter("@Magiaodichorderby", SqlDbType.VarChar), maorder);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_PhieuindongoptheoKH", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_PhieuindongopdanhsachKH(string Madonvi, string madongop, string maorder)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            parameters.Add(new SqlParameter("@Magiaodichorderby", SqlDbType.VarChar), maorder);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_PhieuindongopdanhsachKH", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Phieuindongopmasan(string Madonvi, string madongop)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Phieuindongopmasan", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Giaodichgopdonbanbuon(string Madonvi, string Matuyen, string Magiaodich, string magiaodichorderby)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Matuyen", SqlDbType.VarChar), Matuyen);
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), Magiaodich);
            parameters.Add(new SqlParameter("@Magiaodichorderby", SqlDbType.VarChar), magiaodichorderby);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Giaodichgopdonbanbuon", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_DSTinhthuongnhanvien(DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.VarChar), tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.VarChar), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_DSTinhthuongnhanvien", "DATA", myArr);
        }

        public static DataTable Fill_GiaodichTondauByTungayDenngay(string Trangthai, string Madonvi, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichTondauByTungayDenngay", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_DSgopdon(string Trangthai, string Madonvi, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@DenNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_DSgopdon", "DATA", myArr);
        }
        public static DataTable GetDataLogMart(string Madonvi, string maFormName, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maFormName", SqlDbType.VarChar), maFormName);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataLogMart", "DATA", myArr);
        }
        public static DataTable GetData_VTHH_Soluongkhdongop(string Madonvi, string Matuyen, string Magiaodich, string magiaodichorderby)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Matuyen", SqlDbType.VarChar), Matuyen);
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), Magiaodich);
            parameters.Add(new SqlParameter("@Magiaodichorderby", SqlDbType.VarChar), magiaodichorderby);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Soluongkhdongop", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Soluongkhtheomadongop(string Madonvi, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodich", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Soluongkhtheomadongop", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_MakhachhangAllmagiaodich(string Madonvi, string madongop)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_MakhachhangAllmagiaodich", "DATA", myArr);
        }
        public static DataTable Fill_BangkeThueGtgtByTungayDenngay(int Loai, string Madonvi, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Loai", SqlDbType.VarChar), Loai);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@DenNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_BangkeThueGtgtByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Getdata_THUE_BangkeThueGtgt(int Loai, string Madonvi, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Loai", SqlDbType.Int), Loai);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@DenNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_THUE_BangkeThueGtgt", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichwebByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, string maQuay, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            parameters.Add(new SqlParameter("@maQuay", SqlDbType.VarChar), maQuay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichwebByTungayDenngay", "DATA", myArr);
        }

        public static DataTable GetDataGiaodichwebctByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataGiaodichwebctByMagiaodichpk", "DATA", myArr);
        }

        public static bool Giaodichweb_UpdatetTrangthai(int trangthai, string magiaodichpk, string madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("Giaodichweb_UpdatetTrangthai", myArr);
        }

        public static DataTable Getdata_KT_GiaodichxuathoadonByTungayDenngayMakhoxuat(string Madonvi, DateTime tungay, DateTime denngay, string makhoxuat)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@tungay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@denngay", SqlDbType.DateTime), denngay);
            parameters.Add(new SqlParameter("@Makhoxuat", SqlDbType.VarChar), makhoxuat);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_GiaodichxuathoadonByTungayDenngayMakhoxuat", "DATA", myArr);
        }

        #endregion

        #region các stoed liên quan đến Quy
        public static DataTable GetDataQuychitietByMactpk(string maCtupk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mactktpk", SqlDbType.VarChar), maCtupk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataQuychitietByMactpk", "DATA", myArr);
        }

        public static DataTable GetData_KT_Denghithanhtoanct(string madenghi, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madenghi", SqlDbType.VarChar), madenghi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_KT_Denghithanhtoanct", "DATA", myArr);
        }

        public static DataTable GetdataQuytungaydenngay(string maloaict, string maloaict1, string kieuct, string Madonvi, DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maLoaict", SqlDbType.VarChar), maloaict);
            parameters.Add(new SqlParameter("@maLoaict1", SqlDbType.VarChar), maloaict1);
            parameters.Add(new SqlParameter("@kieuct", SqlDbType.VarChar), kieuct);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetdataQuytungaydenngay", "DATA", myArr);
        }

        public static DataTable Getdata_KT_DSdenghithanhtoan(string Madonvi, DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_DSdenghithanhtoan", "DATA", myArr);
        }

        public static DataTable Getdata_QuyctTaikhoan(string maCtupk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mactupk", SqlDbType.VarChar), maCtupk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_QuyctTaikhoan", "DATA", myArr);
        }

        public static DataTable GetdataQuyButruct(string maCtupk, string Madonvi, string matkno, string matkco)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mactktpk", SqlDbType.VarChar), maCtupk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Matkno", SqlDbType.VarChar), matkno);
            parameters.Add(new SqlParameter("@Matkco", SqlDbType.VarChar), matkco);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataQuyButruchitietByMactpk", "DATA", myArr);
        }

        public static DataTable Getdata_QuyButructTaikhoan(string maCtupk, string Madonvi, string matkno, string matkco)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mactupk", SqlDbType.VarChar), maCtupk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Matkno", SqlDbType.VarChar), matkno);
            parameters.Add(new SqlParameter("@Matkco", SqlDbType.VarChar), matkco);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_QuyButructTaikhoan", "DATA", myArr);
        }

        public static DataTable Getthuchitratruoc(string maKhachhang, string Madonvi, string loaict1, string loaict2, string loaict3, string kieuct1, string kieuct2, string matk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), maKhachhang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Loaict1", SqlDbType.VarChar), loaict1);
            parameters.Add(new SqlParameter("@Loaict2", SqlDbType.VarChar), loaict2);
            parameters.Add(new SqlParameter("@Loaict3", SqlDbType.VarChar), loaict3);
            parameters.Add(new SqlParameter("@Kieuct1", SqlDbType.VarChar), kieuct1);
            parameters.Add(new SqlParameter("@Kieuct2", SqlDbType.VarChar), kieuct2);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), matk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getthuchitratruoc", "DATA", myArr);
        }

        public static DataTable GetdataQuyDoitruct(string maCtupk, string Madonvi, int ThuchiGiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mactktpk", SqlDbType.VarChar), maCtupk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@ThuchiGiaodich", SqlDbType.Int), ThuchiGiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataQuydoitruchitietByMactpk", "DATA", myArr);
        }

        public static DataTable Getdata_KT_Sodutkketchuyen(string Madonvi, string maTk, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), maTk);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_Sodutkketchuyen", "DATA", myArr);
        }

        public static DataTable GetDataDmTaikhoanKCByMadonvi(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataDmTaikhoanKCByMadonvi", "DATA", myArr);
        }

        public static DataTable Getdata_KT_DSDoitrucongnoNDT(string maloaict, string maloaict1, string kieuct, string Madonvi, DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maLoaict", SqlDbType.VarChar), maloaict);
            parameters.Add(new SqlParameter("@maLoaict1", SqlDbType.VarChar), maloaict1);
            parameters.Add(new SqlParameter("@kieuct", SqlDbType.VarChar), kieuct);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_DSButrucongnonhieudoituong", "DATA", myArr);
        }

        public static DataTable Getdata_KT_GhisobutrucongnoNDT(string maCtupk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mactupk", SqlDbType.VarChar), maCtupk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_GhisobutrucongnoNDT", "DATA", myArr);
        }

        public static DataTable Getthuchitratruoc(string maKhachhang, string Madonvi, string loaict1, string loaict2, string loaict3,
            string kieuct1, string kieuct2, string matk, string nhomptnx, DateTime ngaybatdauHT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), maKhachhang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Loaict1", SqlDbType.VarChar), loaict1);
            parameters.Add(new SqlParameter("@Loaict2", SqlDbType.VarChar), loaict2);
            parameters.Add(new SqlParameter("@Loaict3", SqlDbType.VarChar), loaict3);
            parameters.Add(new SqlParameter("@Kieuct1", SqlDbType.VarChar), kieuct1);
            parameters.Add(new SqlParameter("@Kieuct2", SqlDbType.VarChar), kieuct2);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), matk);
            parameters.Add(new SqlParameter("@Nhomptnx", SqlDbType.VarChar), nhomptnx);
            parameters.Add(new SqlParameter("@ngaybatdauHT", SqlDbType.DateTime), ngaybatdauHT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getthuchitratruoc", "DATA", myArr);
        }

        public static DataTable GetCtucongno(string makhachang, string madonvi, string manhomptnx, string matk, string loaict1, string loaict2, string loaict3, DateTime ngaybatdauHT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            parameters.Add(new SqlParameter("@Nhomptnx", SqlDbType.VarChar), manhomptnx);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), matk);
            parameters.Add(new SqlParameter("@Loaict1", SqlDbType.VarChar), loaict1);
            parameters.Add(new SqlParameter("@Loaict2", SqlDbType.VarChar), loaict2);
            parameters.Add(new SqlParameter("@Loaict3", SqlDbType.VarChar), loaict3);
            parameters.Add(new SqlParameter("@ngaybatdauHT", SqlDbType.DateTime), ngaybatdauHT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetCtucongno", "DATA", myArr);
        }

        internal static DataTable GetData_Bangketien_Mactpk(string Mactpk, string Madonvi, string Loaict)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mactpk", SqlDbType.DateTime), Mactpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Loaict", SqlDbType.VarChar), Loaict);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Bangketien_Mactpk", "DATA", myArr);
        }


        public static DataTable Getgiaodichchuathanhtoanhangmua(string makhachang, string madonvi, bool trangthai, int tichchat)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), madonvi);
            parameters.Add(new SqlParameter("@Tinhchat", SqlDbType.Int), tichchat);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Bit), trangthai);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getgiaodichchuathanhtoanhangmua", "DATA", myArr);
        }

        public static DataTable Getdata_KT_Tonquy(DateTime Tungay, DateTime Denngay, string Mataikhoan, string Madonvi, string Matknganhang)
        {
            int thang = Tungay.Month;
            int Nam = Tungay.Year;
            DataTable dtThangKs = new DataTable();
            DateTime tungaydk = DateTime.Parse(Nam.ToString() + "-" + thang.ToString() + "-01");
            thang = thang - 1;
            if (thang == 0)
            {
                thang = 12;
                Nam = Nam - 1;
                dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                int.TryParse(dtThangKs.Rows[0][0].ToString(), out thang);
                if (thang == 12)
                {
                    tungaydk = DateTime.Parse((Nam + 1).ToString() + "-" + (1).ToString() + "-01");
                }
                else
                {
                    tungaydk = DateTime.Parse(Nam.ToString() + "-" + (thang + 1).ToString() + "-01");
                }
            }
            else
            {
                dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                int thangchotso = 0;
                int.TryParse(dtThangKs.Rows[0][0].ToString(), out thangchotso);
                //nếu tháng chốt sổ không liền trước tháng hiện tại xem báo cáo thì gán lại
                if (thangchotso < thang) thang = thangchotso;
                if (thang == 0)
                {
                    Nam = Nam - 1;
                    dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                    int.TryParse(dtThangKs.Rows[0][0].ToString(), out thang);
                    if (thang == 12)
                    {
                        tungaydk = DateTime.Parse((Nam + 1).ToString() + "-" + (1).ToString() + "-01");
                    }
                    else
                    {
                        tungaydk = DateTime.Parse((Nam).ToString() + "-" + (thang + 1).ToString() + "-01");
                    }
                }
                else
                {
                    tungaydk = DateTime.Parse(Nam.ToString() + "-" + (thang + 1).ToString() + "-01");
                }
            }
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Tungay.AddDays(-1));
            parameters.Add(new SqlParameter("@Mataikhoan", SqlDbType.VarChar), Mataikhoan);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Matknganhang", SqlDbType.VarChar), Matknganhang);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_Tonquy", "DATA", myArr);
        }

        #endregion

        #region Số dư đầu kỳ
        public static DataTable Getdata_Sodudauky(string Madonvi, int Thang, int Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Sodudauky", "DATA", myArr);
        }

        public static DataTable GetDatachotsoketoan(string Madonvi, int Thang, int Nam, DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDatachotsoketoan", "DATA", myArr);
        }
        #endregion

        #region Xuất nhập tồn
        //lấy tồn kho mặt hàng
        public static DataTable XNT_Gettonkhomathang(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_GETTONKHO", "DATA", myArr);
        }

        public static DataTable XNT_GETTONKHOALL(string DB_NAME_XNT, string TABLEXNT_NAME, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_GETTONKHOALL", "DATA", myArr);
        }

        public static bool XNT_NHAP(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_NHAP", myArr);
        }

        public static bool XNT_XUAT(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_XUAT", myArr);
        }

        public static bool XNT_NHAPTRALAI(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_NHAPTRALAI", myArr);
        }

        public static bool XNT_UPDATEGIAVON(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_UPDATEGIAVON", myArr);
        }
        public static bool XNT_UPDATEGIAVONCHUYENKHO(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_UPDATEGIAVONCHUYENKHO", myArr);
        }
        public static DataTable XNT_CHECKTONKHO(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_CHECKTONKHO", "DATA", myArr);
        }
        public static bool XNT_XUATBALE(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_XUATBALE", myArr);
        }
        public static bool XNT_XUATBALEAM(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_XUATBALEAM", myArr);
        }

        public static bool XNT_DIEUCHUYENKHO(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_DIEUCHUYENKHO", myArr);
        }

        public static bool XNT_NHAPTONDAU(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_NHAPTONDAU", myArr);
        }
        public static bool XNT_BODUYETNHAP(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_BODUYETNHAP", myArr);
        }

        public static bool XNT_BODUYETXUAT(string TABLEXNT_NAME, string Magiaodichpk, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_BODUYETXUAT", myArr);
        }
        #endregion

        #region Các stored liên quan đến giao dịch và ctugoc
        public static DataTable Getdata_GiaodichctTaikhoan(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_GiaodichctTaikhoan", "DATA", myArr);
        }
        public static DataTable Getdata_GiaodichctTaikhoanGiavon(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_GiaodichctTaikhoanGiavon", "DATA", myArr);
        }
        public static DataTable Getdata_GiaodichctTaikhoan_Khonggiavon(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_GiaodichctTaikhoan_Khonggiavon", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_GiaodichctTaikhoanKiemke(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_GiaodichctTaikhoanKiemke", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_GiaodichctTaikhoanQuay(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_GiaodichctTaikhoanQuay", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_GiaodichctTaikhoanQuaytheonv(string Manhanvien, string Madonvi, DateTime Ngayphatsinh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Manhanviencongno", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.DateTime), Ngayphatsinh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_GiaodichctTaikhoanQuaytheonv", "DATA", myArr);
        }

        public static DataTable Getdata_KT_Manvcongnotheongay(string Madonvi, DateTime Ngayphatsinh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonVi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.DateTime), Ngayphatsinh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_Manvcongnotheongay", "DATA", myArr);
        }

        public static DataTable Getdata_GiaodichquayctTaikhoan(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_GiaodichquayctTaikhoan", "DATA", myArr);
        }

        //getdata tài khoản cho phiếu xuất trả
        public static DataTable Getdata_GiaodichXuatTractTaikhoan(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_GiaodichXuatTractTaikhoan", "DATA", myArr);
        }
        //Insert các cặp tài khoản vào ctugoc
        public static bool PRO_VTHH_InsertCtugocByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("PRO_VTHH_InsertCtugocByMagiaodichpk", myArr);
        }

        public static DataTable Getdata_Lainganhangtamtinh(string MaTk, string Madonvi, DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@MaTK", SqlDbType.VarChar), MaTk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Lainganhangtamtinh", "DATA", myArr);
        }

        public static DataTable Getdata_Lichthanhtoanlaivay(string MaTk, string Madonvi, DateTime tungay, DateTime denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@MaTK", SqlDbType.VarChar), MaTk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Lichthanhtoanlaivay", "DATA", myArr);
        }
        #endregion

        #region Các stored chuyển ngày
        public static bool XNT_CREATETABLE(string TABLEXNT_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_CREATETABLE", myArr);
        }
        public static bool XNT_INSERTNEW(string TABLEXNT_NEW, string DB_NAME_XNT, string TABLEXNT_OLD, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NEW", SqlDbType.VarChar), TABLEXNT_NEW);
            parameters.Add(new SqlParameter("@TABLEXNT_OLD", SqlDbType.VarChar), TABLEXNT_OLD);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_INSERTNEW", myArr);
        }
        public static bool XNT_KHOASO_NHAP(string TABLEXNT_NAME, DateTime Ngayphatsinh, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT, string Manhomptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.VarChar), Ngayphatsinh);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Manhomptnx", SqlDbType.VarChar), Manhomptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_KHOASO_NHAP", myArr);
        }

        public static bool XNT_KHOASO_NHAPTONDAU(string TABLEXNT_NAME, DateTime Ngayphatsinh, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT, string Manhomptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.VarChar), Ngayphatsinh);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Manhomptnx", SqlDbType.VarChar), Manhomptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_KHOASO_NHAPTONDAU", myArr);
        }

        public static bool XNT_KHOASO_XUAT(string TABLEXNT_NAME, DateTime Ngayphatsinh, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT, string Manhomptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.VarChar), Ngayphatsinh);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Manhomptnx", SqlDbType.VarChar), Manhomptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_KHOASO_XUAT", myArr);
        }

        public static bool XNT_KHOASO_NHAPTRALAI(string TABLEXNT_NAME, DateTime Ngayphatsinh, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT, string Manhomptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.VarChar), Ngayphatsinh);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Manhomptnx", SqlDbType.VarChar), Manhomptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_KHOASO_NHAPTRALAI", myArr);
        }
        public static bool XNT_KHOASO_XUATBANLE(string TABLEXNT_NAME, DateTime Ngayphatsinh, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT, string Manhomptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.VarChar), Ngayphatsinh);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Manhomptnx", SqlDbType.VarChar), Manhomptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_KHOASO_XUATBANLE", myArr);
        }
        public static bool XNT_KHOASO_DIEUCHUYENKHO(string TABLEXNT_NAME, DateTime Ngayphatsinh, string Madonvi, string Trangthai, string DB_NAME, string DB_NAME_XNT, string Manhomptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.VarChar), Ngayphatsinh);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Manhomptnx", SqlDbType.VarChar), Manhomptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_KHOASO_DIEUCHUYENKHO", myArr);
        }

        public static bool CREATE_XNT_VIEW(string TABLEXNT_NAME, DateTime Ngayphatsinh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TBXNT", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.VarChar), Ngayphatsinh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("CREATE_XNT_VIEW", myArr);
        }

        public static bool XNT_INSERTNEW(string TABLEXNT_NEW, string DB_NAME_XNT, string TABLEXNT_OLD, string Madonvi, DateTime Ngayphatsinh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NEW", SqlDbType.VarChar), TABLEXNT_NEW);
            parameters.Add(new SqlParameter("@TABLEXNT_OLD", SqlDbType.VarChar), TABLEXNT_OLD);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.VarChar), Ngayphatsinh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("XNT_INSERTNEW", myArr);
        }

        #endregion

        #region Các stored lấy số lượng tồn
        public static DataTable XNT_GETTONBOHANG(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Mabohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Mabohang", SqlDbType.VarChar), Mabohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_GETTONBOHANG", "DATA", myArr);
        }
        public static DataTable XNT_GETTONKHO(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_GETTONKHO", "DATA", myArr);
        }
        public static DataTable XNT_GETTONDATHANG(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manganh, string Manhomhang
            , string Makhachhang, DateTime tuNgay, DateTime denNgay, string ManhomptnxXuatKhac, string ManhomptnxNhapKhac, string ManhomPtnxXuatban, string ManhomPtnxNhapmua)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            parameters.Add(new SqlParameter("@ManhomptnxXuat", SqlDbType.VarChar), ManhomptnxXuatKhac);
            parameters.Add(new SqlParameter("@ManhomptnxNhap", SqlDbType.VarChar), ManhomptnxNhapKhac);
            parameters.Add(new SqlParameter("@ManhomPtnxXuatban", SqlDbType.VarChar), ManhomPtnxXuatban);
            parameters.Add(new SqlParameter("@ManhomPtnxNhapmua", SqlDbType.VarChar), ManhomPtnxNhapmua);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_GETTONDATHANG", "DATA", myArr);
        }
        public static DataTable XNT_GETTONDATHANG_MASIEUTHI(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manganh, string Manhomhang
            , string Makhachhang, DateTime tuNgay, DateTime denNgay, string ManhomptnxXuatKhac, string ManhomptnxNhapKhac, string ManhomPtnxXuatban, string ManhomPtnxNhapmua
            , string strMasieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            parameters.Add(new SqlParameter("@ManhomptnxXuat", SqlDbType.VarChar), ManhomptnxXuatKhac);
            parameters.Add(new SqlParameter("@ManhomptnxNhap", SqlDbType.VarChar), ManhomptnxNhapKhac);
            parameters.Add(new SqlParameter("@ManhomPtnxXuatban", SqlDbType.VarChar), ManhomPtnxXuatban);
            parameters.Add(new SqlParameter("@ManhomPtnxNhapmua", SqlDbType.VarChar), ManhomPtnxNhapmua);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), strMasieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_GETTONDATHANG_MASIEUTHI", "DATA", myArr);
        }
        public static DataTable XNT_PHANTICHDENGHIXUATHANG(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manganh, string Manhomhang
            , string Makhachhang, DateTime tuNgay, DateTime denNgay, string ManhomptnxXuatKhac, string ManhomptnxNhapKhac
            , string ManhomPtnxXuatban, string ManhomPtnxNhapmua, string strMakhoxuat, string TABLEXNT_NAME_TON)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            parameters.Add(new SqlParameter("@ManhomptnxXuat", SqlDbType.VarChar), ManhomptnxXuatKhac);
            parameters.Add(new SqlParameter("@ManhomptnxNhap", SqlDbType.VarChar), ManhomptnxNhapKhac);
            parameters.Add(new SqlParameter("@ManhomPtnxXuatban", SqlDbType.VarChar), ManhomPtnxXuatban);
            parameters.Add(new SqlParameter("@ManhomPtnxNhapmua", SqlDbType.VarChar), ManhomPtnxNhapmua);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@TABLEXNT_TON", SqlDbType.VarChar), TABLEXNT_NAME_TON);
            parameters.Add(new SqlParameter("@Makhoxuat", SqlDbType.VarChar), strMakhoxuat);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_PHANTICHDENGHIXUATHANG", "DATA", myArr);
        }
        public static DataTable XNT_PHANTICHKEHOACHDATHANG(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manganh, string Manhomhang
            , string Makhachhang, DateTime tuNgay, DateTime denNgay, string ManhomptnxXuatKhac, string ManhomptnxNhapKhac, string ManhomPtnxXuatban, string ManhomPtnxNhapmua
            , string strMakhoxuat, string TABLEXNT_NAME_TON)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            parameters.Add(new SqlParameter("@ManhomptnxXuat", SqlDbType.VarChar), ManhomptnxXuatKhac);
            parameters.Add(new SqlParameter("@ManhomptnxNhap", SqlDbType.VarChar), ManhomptnxNhapKhac);
            parameters.Add(new SqlParameter("@ManhomPtnxXuatban", SqlDbType.VarChar), ManhomPtnxXuatban);
            parameters.Add(new SqlParameter("@ManhomPtnxNhapmua", SqlDbType.VarChar), ManhomPtnxNhapmua);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@TABLEXNT_TON", SqlDbType.VarChar), TABLEXNT_NAME_TON);
            parameters.Add(new SqlParameter("@Makhoxuat", SqlDbType.VarChar), strMakhoxuat);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_PHANTICHKEHOACHDATHANG", "DATA", myArr);
        }
        public static DataTable XNT_GETTONDINHMUCMAXMIN(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manganh, string Manhomhang
            , string Makhachhang, DateTime tuNgay, DateTime denNgay, string ManhomptnxXuat, string ManhomptnxNhap)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            parameters.Add(new SqlParameter("@ManhomptnxXuat", SqlDbType.VarChar), ManhomptnxXuat);
            parameters.Add(new SqlParameter("@ManhomptnxNhap", SqlDbType.VarChar), ManhomptnxNhap);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_GETTONDINHMUCMAXMIN", "DATA", myArr);
        }
        public static DataTable XNT_GETTONDINHMUCMAXMINNOIBO(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manganh, string Manhomhang
            , string Makhachhang, string tuNgay, string denNgay, string ManhomptnxXuat, string ManhomptnxNhap)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.VarChar), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.VarChar), denNgay);
            parameters.Add(new SqlParameter("@ManhomptnxXuat", SqlDbType.VarChar), ManhomptnxXuat);
            parameters.Add(new SqlParameter("@ManhomptnxNhap", SqlDbType.VarChar), ManhomptnxNhap);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_GETTONDINHMUCMAXMINNOIBO", "DATA", myArr);
        }

        public static DataTable XNT_GETTONDINHMUCMAXMINNOIBO_NCCTHEOKHO(string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manganh, string Manhomhang
            , string Makhachhang, string tuNgay, string denNgay, string ManhomptnxXuat, string ManhomptnxNhap)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganh);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.VarChar), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.VarChar), denNgay);
            parameters.Add(new SqlParameter("@ManhomptnxXuat", SqlDbType.VarChar), ManhomptnxXuat);
            parameters.Add(new SqlParameter("@ManhomptnxNhap", SqlDbType.VarChar), ManhomptnxNhap);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_GETTONDINHMUCMAXMINNOIBO_NCCTHEOKHO", "DATA", myArr);
        }
        #endregion

        #region BackupDB
        public static bool BACKUP_DB_FULL(string DBNAME, string filePath)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@DBNAME", SqlDbType.VarChar), DBNAME);
            parameters.Add(new SqlParameter("@path", SqlDbType.VarChar), filePath);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("BACKUP_DB_FULL", myArr);
        }
        internal static DataTable CHECKDRIVE(string strConnBackup)
        {
            ListDictionary parameters = new ListDictionary();
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            SqlConnection Connection = new SqlConnection(strConnBackup);
            SqlCommand comm = new SqlCommand("CHECKDRIVE");
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            foreach (DictionaryEntry paramV in myArr)
            {
                comm.Parameters.AddWithValue(paramV.Key.ToString(), paramV.Value);
            }
            SqlDataAdapter resultDA = new SqlDataAdapter();
            resultDA.SelectCommand = comm;
            resultDA.SelectCommand.Connection = Connection;
            Connection.Open();
            DataSet resultDS = new DataSet();
            try
            {
                resultDA.Fill(resultDS, "DATA");
            }
            catch { return null; }
            finally
            {
                Connection.Close();
            }
            return resultDS.Tables[0];
        }

        public static bool BACKUP_DB_FULL(string DBNAME, string filePath, string fileName, string strConnBackup)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@DBNAME", SqlDbType.VarChar), DBNAME);
            parameters.Add(new SqlParameter("@path", SqlDbType.VarChar), filePath);
            parameters.Add(new SqlParameter("@filename ", SqlDbType.VarChar), fileName);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBoolBackup("BACKUP_DB_FULL", strConnBackup, myArr);
        }
        private static bool ExecuteSPReaderReturnBoolBackup(string StoredProcedure, string strConnBackup, params DictionaryEntry[] ParamName)
        {
            bool isok = false;
            SqlConnection Connection = new SqlConnection(strConnBackup);
            SqlCommand comm = new SqlCommand(StoredProcedure);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            foreach (DictionaryEntry paramV in ParamName)
            {
                comm.Parameters.AddWithValue(paramV.Key.ToString(), paramV.Value);
            }
            SqlDataAdapter resultDA = new SqlDataAdapter();
            resultDA.SelectCommand = comm;
            resultDA.SelectCommand.Connection = Connection;
            Connection.Open();
            try
            {
                comm.ExecuteNonQuery();
                isok = true;
            }
            catch (Exception ex)
            {
                isok = false;
            }
            finally
            {
                Connection.Close();
            }
            return isok;
        }
        #endregion

        #region báo cáo vật tu hàng hóa
        //Tóm tắt bán lẻ
        public static DataTable BC_TOMTATBANLE(DateTime Tungay, DateTime Denngay, string Madonvi, string Manguoitao, string Mayban, int Group, string Makhohang, string Maptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manguoitao", SqlDbType.VarChar), Manguoitao);
            parameters.Add(new SqlParameter("@Mayban", SqlDbType.VarChar), Mayban);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Group);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Maptnx", SqlDbType.VarChar), Maptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_TOMTATBANLE", "DATA", myArr);
        }
        public static DataTable BC_TOMTATBANLE_IN(DateTime Tungay, DateTime Denngay, string Madonvi, string Manguoitao, string Mayban, int Group, string Makhohang, string Maptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manguoitao", SqlDbType.VarChar), Manguoitao);
            parameters.Add(new SqlParameter("@Mayban", SqlDbType.VarChar), Mayban);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Group);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Maptnx", SqlDbType.VarChar), Maptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_TOMTATBANLE_IN", "DATA", myArr);
        }
        //Báo cáo bảng kê hàng tồn kho tổng hợp và chi tiết

        public static DataTable BC_TIENICH_TIMKIEM(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string MaPk, int Loai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mapk", SqlDbType.VarChar), MaPk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Loai", SqlDbType.Int), Loai);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_TIENICH_TIMKIEM", "DATA", myArr);
        }
        public static DataTable BC_HANGTONKHOCT(string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manhacungcap, string Manganhhang
            , string Manhomhang, string Masieuthi, string DB_NAME, string DB_NAME_XNT, string soSanh, string soSanh1, int Groupid, int Trangthaivattu, string Manhomphu)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Sosanh", SqlDbType.VarChar), soSanh);
            parameters.Add(new SqlParameter("@Sosanh1", SqlDbType.VarChar), soSanh1);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Trangthaivt", SqlDbType.Int), Trangthaivattu);
            parameters.Add(new SqlParameter("@Manhomphu", SqlDbType.VarChar), Manhomphu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_HANGTONKHOCT", "DATA", myArr);
        }

        public static DataTable BC_HANGTONKHOSERIAL(string TABLEXNT_NAME, string Madonvi, string Manhacungcap, string Manganhhang
            , string Manhomhang, string Masieuthi, string DB_NAME, string DB_NAME_XNT, int Groupid, string Manhomphu)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manhomphu", SqlDbType.VarChar), Manhomphu);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_HANGTONKHOSERIAL", "DATA", myArr);
        }

        public static DataTable BC_HANGTONKHOTH(string TABLEXNT_NAME, string Makhohang, string Manhacungcap, string Manganhhang, string Manhomhang
            , string Masieuthi, string Madonvi, string DB_NAME, string DB_NAME_XNT, string soSanh, string soSanh1, int Groupid, int Trangthaivattu, string Manhomphu)
        {
            ListDictionary parameters = new ListDictionary();

            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Sosanh", SqlDbType.VarChar), soSanh);
            parameters.Add(new SqlParameter("@Sosanh1", SqlDbType.VarChar), soSanh1);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Trangthaivt", SqlDbType.Int), Trangthaivattu);
            parameters.Add(new SqlParameter("@Manhomphu", SqlDbType.VarChar), Manhomphu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_HANGTONKHOTH", "DATA", myArr);
        }
        public static DataTable BC_HANGTONKHOTH_KHOHANGGIA(string TABLEXNT_NAME, string Makhohang, string Manhacungcap, string Manganhhang, string Manhomhang
            , string Masieuthi, string Madonvi, string DB_NAME, string DB_NAME_XNT, string soSanh, string soSanh1, int Groupid, int Trangthaivattu, string Manhomphu)
        {
            ListDictionary parameters = new ListDictionary();

            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Sosanh", SqlDbType.VarChar), soSanh);
            parameters.Add(new SqlParameter("@Sosanh1", SqlDbType.VarChar), soSanh1);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Trangthaivt", SqlDbType.Int), Trangthaivattu);
            parameters.Add(new SqlParameter("@Manhomphu", SqlDbType.VarChar), Manhomphu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_HANGTONKHOTH_KHOHANGGIA", "DATA", myArr);
        }
        public static DataTable BC_HANGTONKHOTH_KHOHANGGIA_SHOWWEB(string TABLEXNT_NAME, string Makhohang, string Manhacungcap, string Manganhhang, string Manhomhang
           , string Masieuthi, string Madonvi, string DB_NAME, string DB_NAME_XNT, string soSanh, string soSanh1, int Groupid, int Trangthaivattu, string Manhomphu)
        {
            ListDictionary parameters = new ListDictionary();

            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Sosanh", SqlDbType.VarChar), soSanh);
            parameters.Add(new SqlParameter("@Sosanh1", SqlDbType.VarChar), soSanh1);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Trangthaivt", SqlDbType.Int), Trangthaivattu);
            parameters.Add(new SqlParameter("@Manhomphu", SqlDbType.VarChar), Manhomphu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_HANGTONKHOTH_KHOHANGGIA_SHOWWEB", "DATA", myArr);
        }
        public static DataTable BC_HANGTONKHOCTGDLUUTAM(string TABLEXNT_NAME, DateTime Ngayphatsinh, string Makhohang, string Madonvi, string Manhacungcap, string Manganhhang
            , string Manhomhang, string Masieuthi, string DB_NAME, string DB_NAME_XNT, string soSanh, string soSanh1, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Ngayphatsinh", SqlDbType.DateTime), Ngayphatsinh);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Sosanh", SqlDbType.VarChar), soSanh);
            parameters.Add(new SqlParameter("@Sosanh1", SqlDbType.VarChar), soSanh1);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_HANGTONKHOCTGDLUUTAM", "DATA", myArr);
        }
        //Xuất bán le 
        public static DataTable BC_XUATBANLECT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho
            , string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Groupid, string Makhachhang, string Manhomphu)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manhomphu", SqlDbType.VarChar), Manhomphu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANLECT", "DATA", myArr);
        }
        public static DataTable BC_LICHSU_THAYDOIGIABAN(DateTime Tungay, DateTime Denngay, string Madonvi, string Makho
            , string Manganhhang, string Manguoitao, string Masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manguoitao", SqlDbType.VarChar), Manguoitao);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_LICHSU_THAYDOIGIABAN", "DATA", myArr);
        }
        public static DataTable BC_XUATBANLEAMCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Groupid, int Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANLEAMCT", "DATA", myArr);
        }
        public static DataTable BC_XUATBANLESERIAL(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANLESERIAL", "DATA", myArr);
        }
        public static DataTable BC_XUATBANLETH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap
            , string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Groupid, string Makhachhang, string Manhomphu)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manhomphu", SqlDbType.VarChar), Manhomphu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANLETH", "DATA", myArr);
        }
        public static DataTable BC_XUATBANLETH_THUE(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap
            , string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Groupid, string Makhachhang, string Manhomphu)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manhomphu", SqlDbType.VarChar), Manhomphu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANLETH_THUE", "DATA", myArr);
        }
        public static DataTable BC_XUATBANLETH_THUE_BYMAPK(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANLETH_THUE_BYMAPK", "DATA", myArr);
        }
        public static DataTable BC_XUATBANLEAMTH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Groupid, int Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANLEAMTH", "DATA", myArr);
        }
        //Báo cáo nhập hàng
        public static DataTable BC_NHAPHANGCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string mavuviec, int Groupid, int Tachkhuyenmai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Mavuviec", SqlDbType.VarChar), mavuviec);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Tachkhuyenmai", SqlDbType.Int), Tachkhuyenmai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGCT", "DATA", myArr);
        }
        public static DataTable BC_NHAPHANGNOIBOCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string mavuviec, int Groupid, int Tachkhuyenmai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Mavuviec", SqlDbType.VarChar), mavuviec);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Tachkhuyenmai", SqlDbType.Int), Tachkhuyenmai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGNOIBOCT", "DATA", myArr);
        }
        public static DataTable BC_XUATHANGNOIBOCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string mavuviec, int Groupid, int Tachkhuyenmai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Mavuviec", SqlDbType.VarChar), mavuviec);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Tachkhuyenmai", SqlDbType.Int), Tachkhuyenmai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATHANGNOIBOCT", "DATA", myArr);
        }
        public static DataTable BC_NHAPHANGSERIAL(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGSERIAL", "DATA", myArr);
        }

        public static DataTable BC_NHAPHANGTH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Mavuviec, int Groupid, int Tachkhuyenmai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Mavuviec", SqlDbType.VarChar), Mavuviec);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Tachkhuyenmai", SqlDbType.Int), Tachkhuyenmai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGTH", "DATA", myArr);
        }
        public static DataTable BC_NHAPHANGNOIBOTH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Mavuviec, int Groupid, int Tachkhuyenmai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Mavuviec", SqlDbType.VarChar), Mavuviec);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Tachkhuyenmai", SqlDbType.Int), Tachkhuyenmai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGNOIBOTH", "DATA", myArr);
        }
        public static DataTable BC_XUATHANGNOIBOTH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Mavuviec, int Groupid, int Tachkhuyenmai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Mavuviec", SqlDbType.VarChar), Mavuviec);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Tachkhuyenmai", SqlDbType.Int), Tachkhuyenmai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATHANGNOIBOTH", "DATA", myArr);
        }
        //Báo cáo xuất nhập tồn nhiều phát sinh
        public static DataTable XNT_BC_XNT(int Tonghopchitiet, string TABLEXNT_NAME, string TABLEXNT_OLD, string Makhohang, string Madonvi, string Manhacungcap, string Manganhhang
           , string Manhomhang, string Masieuthi, string DB_NAME, string DB_NAME_XNT, int Groupid, DateTime Tungay, DateTime Denngay, int Trangthai
           , string N_MUA, string N_DIEUCHINH, string N_NOIBO, string N_TRALAI, string N_KIEMKE, string N_BANBUONTRALAI, string X_BANBUON, string X_BANLE, string X_NOIBO, string X_CHUYENKHO
           , string X_DIEUCHINH, string X_TRALAI, string X_KHAC, string X_HUY, string X_KIEMKE)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tonghopchitiet", SqlDbType.VarChar), Tonghopchitiet);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@TABLEXNT_OLD", SqlDbType.VarChar), TABLEXNT_OLD);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@N_MUA", SqlDbType.VarChar), N_MUA);
            parameters.Add(new SqlParameter("@N_DIEUCHINH", SqlDbType.VarChar), N_DIEUCHINH);
            parameters.Add(new SqlParameter("@N_NOIBO", SqlDbType.VarChar), N_NOIBO);
            parameters.Add(new SqlParameter("@N_TRALAI", SqlDbType.VarChar), N_TRALAI);
            parameters.Add(new SqlParameter("@N_KIEMKE", SqlDbType.VarChar), N_KIEMKE);
            parameters.Add(new SqlParameter("@N_BANBUONTRALAI", SqlDbType.VarChar), N_BANBUONTRALAI);
            parameters.Add(new SqlParameter("@X_BANBUON", SqlDbType.VarChar), X_BANBUON);
            parameters.Add(new SqlParameter("@X_BANLE", SqlDbType.VarChar), X_BANLE);
            parameters.Add(new SqlParameter("@X_NOIBO", SqlDbType.VarChar), X_NOIBO);
            parameters.Add(new SqlParameter("@X_CHUYENKHO", SqlDbType.VarChar), X_CHUYENKHO);
            parameters.Add(new SqlParameter("@X_DIEUCHINH", SqlDbType.VarChar), X_DIEUCHINH);
            parameters.Add(new SqlParameter("@X_TRALAI", SqlDbType.VarChar), X_TRALAI);
            parameters.Add(new SqlParameter("@X_KHAC", SqlDbType.VarChar), X_KHAC);
            parameters.Add(new SqlParameter("@X_HUY", SqlDbType.VarChar), X_HUY);
            parameters.Add(new SqlParameter("@X_KIEMKE", SqlDbType.VarChar), X_KIEMKE);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_BC_XNTTUNGAYDENNGAY", "DATA", myArr);
        }

        //Báo cáo nhập hàng điều chỉnh
        public static DataTable BC_NHAPHANGDIEUCHINHCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGDIEUCHINHCT", "DATA", myArr);
        }

        public static DataTable BC_NHAPHANGDIEUCHINHSerial(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGDIEUCHINHSERIAL", "DATA", myArr);
        }

        public static DataTable BC_NHAPHANGDIEUCHINHTH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGDIEUCHINHTH", "DATA", myArr);
        }

        //Báo cáo nhập hàng xuất chuyển kho
        public static DataTable BC_CHUYENKHOCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makhoxuat, string makhonhap, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makhoxuat", SqlDbType.VarChar), Makhoxuat);
            parameters.Add(new SqlParameter("@Makhonhap", SqlDbType.VarChar), makhonhap);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_CHUYENKHOCT", "DATA", myArr);
        }
        public static DataTable BC_CHUYENKHOTH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makhoxuat, string makhonhap, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makhoxuat", SqlDbType.VarChar), Makhoxuat);
            parameters.Add(new SqlParameter("@Makhonhap", SqlDbType.VarChar), makhonhap);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_CHUYENKHOTH", "DATA", myArr);
        }

        //Báo cáo xuất bán buôn
        public static DataTable BC_XUATBANBUONCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Makhachhang, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Manhanvien, string Magiaodichdongop, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Magiaodichdongop", SqlDbType.VarChar), Magiaodichdongop);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANBUONCT", "DATA", myArr);
        }
        public static DataTable BC_XUATBANBUONXKCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Makhachhang, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Manhanvien, string Magiaodichdongop, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Magiaodichdongop", SqlDbType.VarChar), Magiaodichdongop);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANBUONXKCT", "DATA", myArr);
        }

        public static DataTable BC_XUATBANBUONTH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string makhachhang, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Manhanvien, string Magiaodichdongop, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachhang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Magiaodichdongop", SqlDbType.VarChar), Magiaodichdongop);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANBUONTH", "DATA", myArr);
        }

        public static DataTable BC_XUATBANBUONSERIAL(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Manhanvien, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANBUONSERIAL", "DATA", myArr);
        }

        public static DataTable BC_XUATBANBUONCTTHEOVATTU(DateTime Tungay, DateTime Denngay, string Madonvi, string Makho, string Manhacungcap, string makhachhang, string Manganhhang, string Manhomhang, string Masieuthi, string madongop, string Manvdathang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachhang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Magiaodichdongop", SqlDbType.VarChar), madongop);
            parameters.Add(new SqlParameter("@Manvdathang", SqlDbType.VarChar), Manvdathang);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATBANBUONCTTHEOVATTU", "DATA", myArr);
        }

        public static DataTable BC_XUATLECTTHEOVATTU(DateTime Tungay, DateTime Denngay, string Madonvi, string Manvdathang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manvdathang", SqlDbType.VarChar), Manvdathang);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATLECTTHEOVATTU", "DATA", myArr);
        }
        // Báo cáo xuất hàng điều chỉnh

        public static DataTable BC_VTHH_XUATHANGDIEUCHINHCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_XUATHANGDIEUCHINHCT", "DATA", myArr);
        }

        public static DataTable BC_VTHH_XUATHANGDIEUCHINHTH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_XUATHANGDIEUCHINHTH", "DATA", myArr);
        }
        //Báo cáo xuất hủy
        public static DataTable BC_XUATHUY_TH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATHUY_TH", "DATA", myArr);
        }
        public static DataTable BC_XUATHUY_CT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATHUY_CT", "DATA", myArr);
        }

        //Báo cáo xuất trả nhà cung cấp
        public static DataTable BC_XUATTRANCC_TH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATTRANCC_TH", "DATA", myArr);
        }
        public static DataTable BC_XUATTRANCC_CT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_XUATTRANCC_CT", "DATA", myArr);
        }

        public static DataTable BC_LAILOTH(DateTime Tungay, DateTime Denngay, string Madonvi, string Makhohang, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_LAILOTH", "DATA", myArr);
        }


        public static DataTable XNT_BC_XNTTHUGON(string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manhacungcap, string Manganhhang
           , string Manhomhang, string Masieuthi, string DB_NAME, string DB_NAME_XNT, int Groupid, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_BC_XNTTUNGAYDENNGAYTHUGON", "DATA", myArr);
        }

        public static DataTable XNT_BC_XNTTachxuatbannhapmua(string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manhacungcap, string Manganhhang
           , string Manhomhang, string Masieuthi, string DB_NAME, string DB_NAME_XNT, int Groupid, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_BC_XNTTachxuatbannhapmua", "DATA", myArr);
        }

        public static DataTable XNT_BC_XNTTachxuatbannhapmuahdkohd(string TABLEXNT_NAME, string Makhohang, string Madonvi, string Manhacungcap, string Manganhhang
           , string Manhomhang, string Masieuthi, string DB_NAME, string DB_NAME_XNT, int Groupid, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("XNT_BC_XNTTachxuatbannhapmuahdkohd", "DATA", myArr);
        }

        public static DataTable BC_NHAPBANBUONTLTH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string makhachhang, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Manhanvien, string magiaodichdongop, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachhang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Magiaodichdongop", SqlDbType.VarChar), magiaodichdongop);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPBANBUONTLTH", "DATA", myArr);
        }

        public static DataTable BC_NHAPBANBUONTLCT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Makhachhang, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Manhanvien, string magiaodichdongop, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Magiaodichdongop", SqlDbType.VarChar), magiaodichdongop);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPBANBUONTLCT", "DATA", myArr);
        }

        public static DataTable BC_NHAPBANBUONTLSERIAL(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, string Manhanvien, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPBANBUONTLSERIAL", "DATA", myArr);
        }

        //Thẻ kho vật tư hàng hóa
        public static DataTable BC_VTHH_THEKHO(DateTime Tungay, DateTime Denngay, string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Manganhhang, string Manhomhang
            , string Masieuthi, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_THEKHO", "DATA", myArr);
        }

        public static DataTable BC_VTHH_THEKHOTHEOGIAODICHTONG(DateTime Tungay, DateTime Denngay, string DB_NAME_XNT, string TABLEXNT_NAME, string Makhohang, string Manganhhang, string Manhomhang
           , string Masieuthi, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_THEKHOTHEOGIAODICHTONG", "DATA", myArr);
        }

        public static DataTable BC_VTHH_SOCHITIETBANHANG(DateTime Tungay, DateTime Denngay, string Makhachhang, string Makhohang, string Manganhhang, string Manhomhang, string Masieuthi, string Madonvi, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_SOCHITIETBANHANG", "DATA", myArr);
        }
        public static DataTable BC_VTHH_S16_DNN(DateTime Tungay, DateTime Denngay, string Makhachhang, string Makhohang, string Manganhhang, string Manhomhang, string Masieuthi, string Madonvi, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_S16_DNN", "DATA", myArr);
        }
        public static DataTable BC_VTHH_SOCHITIETMUAHANG(DateTime Tungay, DateTime Denngay, string Makhachhang, string Makhohang, string Manganhhang, string Manhomhang, string Masieuthi, string Madonvi, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_SOCHITIETMUAHANG", "DATA", myArr);
        }
        public static DataTable BC_VTHH_SONHATKYBANHANG(DateTime Tungay, DateTime Denngay, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_SONHATKYBANHANG", "DATA", myArr);
        }
        public static DataTable BC_VTHH_SONHATKYMUAHANG(DateTime Tungay, DateTime Denngay, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_SONHATKYMUAHANG", "DATA", myArr);
        }
        public static DataTable BC_VTHH_DONCHUAXUAT(DateTime Tungay, DateTime Denngay, string Madonvi, string Makhachhang, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Manhanvienbh", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_DONCHUAXUAT", "DATA", myArr);
        }

        public static DataTable BC_VTHH_KHUYENMAIXUATBAN(DateTime Tungay, DateTime Denngay, string Madonvi, string Manganhhang, string Manhomhang, string Masieuthi, string Manhanvien, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_KHUYENMAIXUATBAN", "DATA", myArr);
        }

        //public static DataTable BC_TONGHOPXUATBANTRALAI(DateTime Tungay, DateTime Denngay, string Madonvi, string Makho, string Manganhhang, string Manhomhang)
        //{
        //    ListDictionary parameters = new ListDictionary();
        //    parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
        //    parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
        //    parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
        //    parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
        //    parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
        //    parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
        //    DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
        //    parameters.CopyTo(myArr, 0);
        //    return ExecuteSPReader("BC_TONGHOPXUATBANTRALAI", "DATA", myArr);
        //}

        public static DataTable BC_TONGHOPXUATBANTRALAI(DateTime Tungay, DateTime Denngay, string Madonvi, string Makhachhang, string Manhanvien, string Magiaodich, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_TONGHOPXUATBANTRALAI", "DATA", myArr);
        }

        public static DataTable BC_CHITIET_XUATBANTRALAI(DateTime Tungay, DateTime Denngay, string Madonvi, string Makhachhang, string Manhanvien, string Magiaodich, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_CHITIET_XUATBANTRALAI", "DATA", myArr);
        }

        public static DataTable BC_TONGHOPXUATBANTRALAITHEODG(DateTime Tungay, DateTime Denngay, string Madonvi, string Makhachhang, string Manhanvien, string Magiaodich, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_TONGHOPXUATBANTRALAITHEODG", "DATA", myArr);
        }

        public static DataTable BC_CHITIET_XUATBANTRALAITHEODG(DateTime Tungay, DateTime Denngay, string Madonvi, string Makhachhang, string Manhanvien, string Magiaodich, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_CHITIET_XUATBANTRALAITHEODG", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_ExportexcelKH(string Madonvi, string madongop)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_ExportexcelKH", "DATA", myArr);
        }

        public static DataTable BC_Chenhlechgiavon(string TABLEXNT_NAME, string TABLEXNT_NAME2, string Makhohang, string Manhacungcap, string Manganhhang, string Manhomhang, int Trangthaivattu
            , string Masieuthi, string Madonvi, string DB_NAME, string DB_NAME_XNT, string Manhomphu)
        {
            ListDictionary parameters = new ListDictionary();

            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME2", SqlDbType.VarChar), TABLEXNT_NAME2);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@DB_NAME", SqlDbType.VarChar), DB_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Trangthaivt", SqlDbType.Int), Trangthaivattu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_Chenhlechgiavon", "DATA", myArr);
        }

        public static DataTable Getdata_Vattukhongphatsinhgiaodich(string TABLEXNT_NAME, string Madonvi, string Manhacungcap, string Manganhhang
            , string Manhomhang, string DB_NAME_XNT, DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mancc", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganh", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhom", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Vattukhongphatsinhgiaodich", "DATA", myArr);
        }

        public static DataTable BC_VTHH_TICHLUYDIEMTHUONG(DateTime Tungay, DateTime Denngay, string Madonvi, string Makhachhang, string Makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TuNgay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@DenNgay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_TICHLUYDIEMTHUONG", "DATA", myArr);
        }

        public static DataTable BC_VTHH_TICHLUYDIEMTHUONGTHEOKH(DateTime Tungay, DateTime Denngay, string Madonvi, string Makhachhang, string Makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@TuNgay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@DenNgay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_TICHLUYDIEMTHUONGTHEOKH", "DATA", myArr);
        }

        public static DataTable BC_VTHH_Tichdiemchitiettheokhachhang(DateTime Tungay, DateTime Denngay, string Madonvi, string Makhachhang, string Makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_Tichdiemchitiettheokhachhang", "DATA", myArr);
        }

        #endregion

        #region Báo có kế toán
        //Sổ tài khoản chữ T
        public static DataTable BC_KT_SoTKChuT(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Matk, string Madonvi, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_SoTKChuT", "DATA", myArr);
        }
        //Sổ nhật ký chung
        public static DataTable BC_KT_SoNhatKyChung(DateTime Tungay, DateTime Denngay, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_SoNhatKyChung", "DATA", myArr);
        }

        //Sổ cái
        public static DataTable BC_KT_SoCai(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Matkno, string Matkco, string Matkdauky, string Madonvi, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matkno", SqlDbType.VarChar), Matkno);
            parameters.Add(new SqlParameter("@Matkco", SqlDbType.VarChar), Matkco);
            parameters.Add(new SqlParameter("@Matkdauky", SqlDbType.VarChar), Matkdauky);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_SoCai", "DATA", myArr);
        }

        //Sổ Chi tiết tk
        public static DataTable BC_KT_SoChiTietTK(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Matk, string Madonvi, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_SoChiTietTK", "DATA", myArr);
        }

        public static DataTable BC_KT_CONGNOTH(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Matk
            , string Madonvi, string madoituong, string Thang, string Nam, string Mangoaite)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            parameters.Add(new SqlParameter("@Mangoaite", SqlDbType.VarChar), Mangoaite);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNOTH", "DATA", myArr);
        }
        public static DataTable BC_KT_CONGNOTH_ROOT(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Matk
            , string Madonvi, string madoituong, string Thang, string Nam, string Mangoaite)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            parameters.Add(new SqlParameter("@Mangoaite", SqlDbType.VarChar), Mangoaite);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNOTH_ROOT", "DATA", myArr);
        }
        public static DataTable BC_KT_CONGNO_NHANVIEN_TH(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Matk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNO_NHANVIEN_TH", "DATA", myArr);
        }
        public static DataTable BC_KT_CONGNOCT(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk
            , string Matk, string Madonvi, string madoituong, string Thang, string Nam, string Mangoaite)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            parameters.Add(new SqlParameter("@Mangoaite", SqlDbType.VarChar), Mangoaite);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNOCT", "DATA", myArr);
        }
        public static DataTable BC_KT_S14_DNN(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk
            , string Matk, string Madonvi, string madoituong, string Thang, string Nam, string Mangoaite)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            parameters.Add(new SqlParameter("@Mangoaite", SqlDbType.VarChar), Mangoaite);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_S14_DNN", "DATA", myArr);
        }
        public static DataTable BC_KT_Doichieucongno(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk
            , string Matk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_Doichieucongno", "DATA", myArr);
        }
        public static DataTable BC_KT_DoichieucongnoTrungThanh(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk
            , string Matk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_DoichieucongnoTrungThanh", "DATA", myArr);
        }
        public static DataTable BC_KT_DoichieucongnoTatThanhDL(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk
            , string Matk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_DoichieucongnoTatThanhDL", "DATA", myArr);
        }
        public static DataTable BC_KT_DoichieucongnoTatThanhDLTH(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk
            , string Matk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_DoichieucongnoTatThanhDLTH", "DATA", myArr);
        }
        public static DataTable BC_KT_DoichieucongnoTatThanhSALE(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk
            , string Matk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_DoichieucongnoTatThanhSALE", "DATA", myArr);
        }
        public static DataTable BC_VTHH_DoanhsoTrungThanh(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk
            , string Matk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_DoanhsoTrungThanh", "DATA", myArr);
        }
        public static DataTable BC_KT_CONGNOCT_ROOT(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk
            , string Matk, string Madonvi, string madoituong, string Thang, string Nam, string Mangoaite)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            parameters.Add(new SqlParameter("@Mangoaite", SqlDbType.VarChar), Mangoaite);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNOCT_ROOT", "DATA", myArr);
        }
        public static DataTable BC_KT_CONGNO_NHANVIEN_CT(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Matk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNO_NHANVIEN_CT", "DATA", myArr);
        }


        public static DataTable BC_KT_CONGNOALLTH(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNOALLTH", "DATA", myArr);
        }

        public static DataTable BC_KT_CONGNOALLCT(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNOALLCT", "DATA", myArr);
        }
        public static DataTable BC_PHIEUTHUCHI_TH(DateTime Tungay, DateTime Denngay, string Madonvi, string Maloaict, int Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maloaict", SqlDbType.VarChar), Maloaict);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_PHIEUTHUCHI_TH", "DATA", myArr);
        }
        public static DataTable BC_PHIEUTHUCHI_CT(DateTime Tungay, DateTime Denngay, string Madonvi, string Maloaict, int Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maloaict", SqlDbType.VarChar), Maloaict);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_PHIEUTHUCHI_CT", "DATA", myArr);
        }

        public static DataTable BC_TONGHOPKHOANMUCCHIPHI(DateTime Tungay, DateTime Denngay, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_TONGHOPKHOANMUCCHIPHI", "DATA", myArr);
        }

        public static DataTable BC_KT_KHOANMUCCHIPHI_TH(DateTime Tungay, DateTime Denngay, string Madonvi, string Makmcp, string Matk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makmcp", SqlDbType.VarChar), Makmcp);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_KHOANMUCCHIPHI_TH", "DATA", myArr);
        }

        public static DataTable BC_KT_KHOANMUCCHIPHI_CT(DateTime Tungay, DateTime Denngay, string Madonvi, string Makmcp, string Matk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Makmcp", SqlDbType.VarChar), Makmcp);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_KHOANMUCCHIPHI_CT", "DATA", myArr);
        }

        public static DataTable BC_KETQUAKINHDOANH(DateTime Tungay, DateTime Denngay, string Madonvi, DateTime Tungaydk, DateTime Denngaydk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KETQUAKINHDOANH", "DATA", myArr);
        }

        public static DataTable BC_BANGCANDOIPHATSINH(DateTime Tungay, DateTime Denngay, string Madonvi, DateTime Tungaydk, DateTime Denngaydk, int Nam, int Thang, string strMatk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.Int), Thang);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), strMatk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_BANGCANDOIPHATSINH", "DATA", myArr);
        }

        public static DataTable BC_QUYTIENMATHTKT_TH(DateTime Tungay, DateTime Denngay, string Mataikhoan, string Madonvi)
        {
            int thang = Tungay.Month;
            int Nam = Tungay.Year;
            DataTable dtThangKs = new DataTable();
            DateTime tungaydk = DateTime.Parse(Nam.ToString() + "-" + thang.ToString() + "-01");
            thang = thang - 1;
            if (thang == 0)
            {
                thang = 12;
                Nam = Nam - 1;
                dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                int.TryParse(dtThangKs.Rows[0][0].ToString(), out thang);
                if (thang == 12)
                {
                    tungaydk = DateTime.Parse((Nam + 1).ToString() + "-" + (1).ToString() + "-01");
                }
                else
                {
                    tungaydk = DateTime.Parse(Nam.ToString() + "-" + (thang + 1).ToString() + "-01");
                }
            }
            else
            {
                dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                int thangchotso = 0;
                int.TryParse(dtThangKs.Rows[0][0].ToString(), out thangchotso);
                //nếu tháng chốt sổ không liền trước tháng hiện tại xem báo cáo thì gán lại
                if (thangchotso < thang) thang = thangchotso;
                if (thang == 0)
                {
                    Nam = Nam - 1;
                    dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                    int.TryParse(dtThangKs.Rows[0][0].ToString(), out thang);
                    if (thang == 12)
                    {
                        tungaydk = DateTime.Parse((Nam + 1).ToString() + "-" + (1).ToString() + "-01");
                    }
                    else
                    {
                        tungaydk = DateTime.Parse((Nam).ToString() + "-" + (thang + 1).ToString() + "-01");
                    }
                }
                else
                {
                    tungaydk = DateTime.Parse(Nam.ToString() + "-" + (thang + 1).ToString() + "-01");
                }
            }
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Tungay.AddDays(-1));
            parameters.Add(new SqlParameter("@Mataikhoan", SqlDbType.VarChar), Mataikhoan);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_QUYTIENMATHTKT_TH", "DATA", myArr);
        }

        public static DataTable BC_KT_QUYTIENMATHTKT_THTP(DateTime Tungay, DateTime Denngay, string Mataikhoan, string Madonvi)
        {
            int thang = Tungay.Month;
            int Nam = Tungay.Year;
            DataTable dtThangKs = new DataTable();
            DateTime tungaydk = DateTime.Parse(Nam.ToString() + "-" + thang.ToString() + "-01");
            thang = thang - 1;
            if (thang == 0)
            {
                thang = 12;
                Nam = Nam - 1;
                dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                int.TryParse(dtThangKs.Rows[0][0].ToString(), out thang);
                if (thang == 12)
                {
                    tungaydk = DateTime.Parse((Nam + 1).ToString() + "-" + (1).ToString() + "-01");
                }
                else
                {
                    tungaydk = DateTime.Parse(Nam.ToString() + "-" + (thang + 1).ToString() + "-01");
                }
            }
            else
            {
                dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                int thangchotso = 0;
                int.TryParse(dtThangKs.Rows[0][0].ToString(), out thangchotso);
                //nếu tháng chốt sổ không liền trước tháng hiện tại xem báo cáo thì gán lại
                if (thangchotso < thang) thang = thangchotso;
                if (thang == 0)
                {
                    Nam = Nam - 1;
                    dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                    int.TryParse(dtThangKs.Rows[0][0].ToString(), out thang);
                    if (thang == 12)
                    {
                        tungaydk = DateTime.Parse((Nam + 1).ToString() + "-" + (1).ToString() + "-01");
                    }
                    else
                    {
                        tungaydk = DateTime.Parse((Nam).ToString() + "-" + (thang + 1).ToString() + "-01");
                    }
                }
                else
                {
                    tungaydk = DateTime.Parse(Nam.ToString() + "-" + (thang + 1).ToString() + "-01");
                }
            }
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Tungay.AddDays(-1));
            parameters.Add(new SqlParameter("@Mataikhoan", SqlDbType.VarChar), Mataikhoan);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_QUYTIENMATHTKT_THTP", "DATA", myArr);
        }

        public static DataTable BC_KT_BANGKEPHIEU_THUCHI(DateTime Tungay, DateTime Denngay, string Maloaict, string Madonvi, int Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Maloaict", SqlDbType.VarChar), Maloaict);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_BANGKEPHIEU_THUCHI", "DATA", myArr);
        }

        public static DataTable BC_KT_BANGKEPHIEU_THUCHINGANHANG(DateTime Tungay, DateTime Denngay, string Maloaict, string Madonvi, int Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Maloaict", SqlDbType.VarChar), Maloaict);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_BANGKEPHIEU_THUCHINGANHANG", "DATA", myArr);
        }
        public static DataTable BC_QUYTIENMATHTKT_CT(DateTime Tungay, DateTime Denngay, string Mataikhoan, string Madonvi)
        {
            int thang = Tungay.Month;
            int Nam = Tungay.Year;
            DataTable dtThangKs = new DataTable();
            DateTime tungaydk = DateTime.Parse(Nam.ToString() + "-" + thang.ToString() + "-01");
            thang = thang - 1;
            if (thang == 0)
            {
                thang = 12;
                Nam = Nam - 1;
                dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                int.TryParse(dtThangKs.Rows[0][0].ToString(), out thang);
                if (thang == 12)
                {
                    tungaydk = DateTime.Parse((Nam + 1).ToString() + "-" + (1).ToString() + "-01");
                }
                else
                {
                    tungaydk = DateTime.Parse(Nam.ToString() + "-" + (thang + 1).ToString() + "-01");
                }
            }
            else
            {
                dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                int thangchotso = 0;
                int.TryParse(dtThangKs.Rows[0][0].ToString(), out thangchotso);
                //nếu tháng chốt sổ không liền trước tháng hiện tại xem báo cáo thì gán lại
                if (thangchotso < thang) thang = thangchotso;
                if (thang == 0)
                {
                    Nam = Nam - 1;
                    dtThangKs = DB.Getdata_KT_MaxThangChotSo(Madonvi, Nam);
                    int.TryParse(dtThangKs.Rows[0][0].ToString(), out thang);
                    if (thang == 12)
                    {
                        tungaydk = DateTime.Parse((Nam + 1).ToString() + "-" + (1).ToString() + "-01");
                    }
                    else
                    {
                        tungaydk = DateTime.Parse((Nam).ToString() + "-" + (thang + 1).ToString() + "-01");
                    }
                }
                else
                {
                    tungaydk = DateTime.Parse(Nam.ToString() + "-" + (thang + 1).ToString() + "-01");
                }
            }

            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Tungay.AddDays(-1));
            parameters.Add(new SqlParameter("@Mataikhoan", SqlDbType.VarChar), Mataikhoan);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_QUYTIENMATHTKT_CT", "DATA", myArr);
        }

        public static DataTable BC_KT_CONGNO_TH_NVKH(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Matk, string Madonvi, string madoituong, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNO_TH_NVKH", "DATA", myArr);
        }

        public static DataTable BC_KT_CONGNO_TH_KHTHEONV(DateTime Tungay, DateTime Denngay, DateTime Tungaydk, DateTime Denngaydk, string Matk, string Madonvi, string madoituong, string manhanvien, string Thang, string Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Matk", SqlDbType.VarChar), Matk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituong", SqlDbType.VarChar), madoituong);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), manhanvien);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.VarChar), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.VarChar), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_CONGNO_TH_KHTHEONV", "DATA", myArr);
        }

        public static bool VTHH_TINHGIAVON(string Madonvi, DateTime Tungay, DateTime Denngay, string Pttinhgia, string TABLEXNT_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Pttinhgia", SqlDbType.VarChar), Pttinhgia);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("VTHH_TINHGIAVON", myArr);
        }

        public static bool VTHH_TINHGIAVON_TACHKHO(string Madonvi, DateTime Tungay, DateTime Denngay, string Pttinhgia, string TABLEXNT_NAME, string DB_NAME_XNT)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Pttinhgia", SqlDbType.VarChar), Pttinhgia);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("VTHH_TINHGIAVON_TACHKHO", myArr);
        }

        public static bool VTHH_TINHGIAVON_NEW(string Madonvi, DateTime Tungay, DateTime Denngay, string Pttinhgia, string TABLEXNT_NAME, string DB_NAME_XNT, int iStep
            , int Thang, int Nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Pttinhgia", SqlDbType.VarChar), Pttinhgia);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@iStep", SqlDbType.Int), iStep);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.Int), Thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("VTHH_TINHGIAVON_NEW", myArr);
        }

        public static DataTable BC_KT_Tienguinganhangtheosotaikhoan(DateTime Tungay, DateTime Denngay, string Madonvi, string matk, string matknganhang, DateTime Tungaydk, DateTime Denngaydk, int Nam, int Thang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mataikhoan", SqlDbType.VarChar), matk);
            parameters.Add(new SqlParameter("@Matknganhang", SqlDbType.VarChar), matknganhang);
            parameters.Add(new SqlParameter("@Tungaydk", SqlDbType.DateTime), Tungaydk);
            parameters.Add(new SqlParameter("@Denngaydk", SqlDbType.DateTime), Denngaydk);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.Int), Thang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_KT_Tienguinganhangtheosotaikhoan", "DATA", myArr);
        }

        public static DataTable Getdata_KT_Kiemtrachotso(string Madonvi, string kieuchotso, int Nam, int Thang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Kieuchotso", SqlDbType.VarChar), kieuchotso);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), Nam);
            parameters.Add(new SqlParameter("@Thang", SqlDbType.Int), Thang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_Kiemtrachotso", "DATA", myArr);
        }
        #endregion

        #region Phiếu in vinamilk
        public static DataTable Getdata_VTHH_Phieuinxbkmvinamilk(string Madonvi, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Phieuinxbkmvinamilk", "DATA", myArr);

        }

        public static DataTable Getdata_VTHH_Phieuinxbvinamilk(string Madonvi, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Phieuinxbvinamilk", "DATA", myArr);

        }
        #endregion

        #region Phiếu in masan
        public static DataTable Getdata_VTHH_Phieuinxbkmmasan(string Madonvi, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Phieuinxbkmmasan", "DATA", myArr);

        }

        public static DataTable Getdata_VTHH_Phieuinxbmasan(string Madonvi, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Phieuinxbmasan", "DATA", myArr);

        }
        #endregion

        #region Phiếu in nuti
        public static DataTable Getdata_VTHH_Phieuinxbnuti(string Madonvi, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Phieuinxbnuti", "DATA", myArr);
        }

        public static DataTable Getdata_VTHH_Phieuinxbkmnuti(string Madonvi, string Magiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodich);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Phieuinxbkmnuti", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_Phieuindongopnuti(string Madonvi, string madongop)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_Phieuindongopnuti", "DATA", myArr);
        }

        public static DataTable GetData_VTHH_PhieuindongoptheoKHnuti(string Madonvi, string madongop, string maorder)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), madongop);
            parameters.Add(new SqlParameter("@Magiaodichorderby", SqlDbType.VarChar), maorder);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_VTHH_PhieuindongoptheoKHnuti", "DATA", myArr);
        }
        #endregion

        #region Báo cáo biểu đồ
        public static DataTable BC_COLUMCHARNHAPHANGTH(string tuthang, string denthang, string tunam, string dennam, string nam, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, int Trangthai, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tuthang", SqlDbType.Int), tuthang);
            parameters.Add(new SqlParameter("@Denthang", SqlDbType.Int), denthang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), nam);
            parameters.Add(new SqlParameter("@Tunam", SqlDbType.Int), tunam);
            parameters.Add(new SqlParameter("@Dennam", SqlDbType.Int), dennam);

            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_COLUMCHARNHAPHANGTH", "DATA", myArr);
        }

        public static DataTable BC_COLUMCHARTXUATBANBUONTH(string tuthang, string denthang, string tunam, string dennam, string nam, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string makhachhang, string Manganhhang, string Manhomhang, int Trangthai, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tuthang", SqlDbType.Int), tuthang);
            parameters.Add(new SqlParameter("@Denthang", SqlDbType.Int), denthang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), nam);
            parameters.Add(new SqlParameter("@Tunam", SqlDbType.Int), tunam);
            parameters.Add(new SqlParameter("@Dennam", SqlDbType.Int), dennam);

            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Makhachhang", SqlDbType.VarChar), makhachhang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_COLUMCHARTXUATBANBUONTH", "DATA", myArr);
        }

        public static DataTable BC_COLUMCHARBANLETH(string tuthang, string denthang, string tunam, string dennam, string nam, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string manguoitao, int Trangthai, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tuthang", SqlDbType.Int), tuthang);
            parameters.Add(new SqlParameter("@Denthang", SqlDbType.Int), denthang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), nam);
            parameters.Add(new SqlParameter("@Tunam", SqlDbType.Int), tunam);
            parameters.Add(new SqlParameter("@Dennam", SqlDbType.Int), dennam);

            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), manguoitao);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_COLUMCHARBANLETH", "DATA", myArr);
        }
        #endregion

        #region Vẩn tải
        public static DataTable Get_VT_Lichxe(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Get_VT_Lichxe", "DATA", myArr);

        }

        #endregion

        #region Sản xuất

        public static DataTable GetDataBaogiactByMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataBaogiactByMagiaodichpk", "DATA", myArr);
        }
        public static DataTable GetData_Sx_DondatctByMapkAndMadonvi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_DondatctByMapkAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_Sx_DondatctNCCByMapkAndMadonvi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_DondatctNCCByMapkAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_Sx_LenhsxctByMapkAndMadonvi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_LenhsxctByMapkAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_Sx_LenhsxctByMasieuthiAndMadonvi(string Masieuthi, string Madonvi, string Mactpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mactpk", SqlDbType.VarChar), Mactpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_LenhsxctByMasieuthiAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_Sx_DenghilinhlieuctByMapkAndMadonvi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_DenghilinhlieuctByMapkAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_Sx_DutinhNVLctByMapkAndMadonvi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_DutinhNVLctByMapkAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_Sx_DutinhNVLct_Denghi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_DutinhNVLct_Denghi", "DATA", myArr);
        }
        public static DataTable GetData_Sx_MaThanhphamByListMagiaodichpk(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_MaThanhphamByListMagiaodichpk", "DATA", myArr);
        }
        public static DataTable GetData_Sx_TinhToanDutinhNVL(string DBXNT_NAME, string Madonvi, string TBNXT_NAME, string Dondat, string Thanhpham, string Khohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@DBXNT_NAME", SqlDbType.VarChar), DBXNT_NAME);
            parameters.Add(new SqlParameter("@TBNXT_NAME", SqlDbType.VarChar), TBNXT_NAME);
            parameters.Add(new SqlParameter("@Dondat", SqlDbType.VarChar), Dondat);
            parameters.Add(new SqlParameter("@Thanhpham", SqlDbType.VarChar), Thanhpham);
            parameters.Add(new SqlParameter("@Khohang", SqlDbType.VarChar), Khohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_TinhToanDutinhNVL", "DATA", myArr);
        }
        public static DataTable GetData_Sx_TinhToanXuatkhoNVL(string DBXNT_NAME, string Madonvi, string TBNXT_NAME, string Dondat, string Thanhpham, string Khohang, decimal Soluong)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@DBXNT_NAME", SqlDbType.VarChar), DBXNT_NAME);
            parameters.Add(new SqlParameter("@TBNXT_NAME", SqlDbType.VarChar), TBNXT_NAME);
            parameters.Add(new SqlParameter("@Dondat", SqlDbType.VarChar), Dondat);
            parameters.Add(new SqlParameter("@Thanhpham", SqlDbType.VarChar), Thanhpham);
            parameters.Add(new SqlParameter("@Khohang", SqlDbType.VarChar), Khohang);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Soluong", SqlDbType.Decimal), Soluong);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_TinhToanXuatkhoNVL", "DATA", myArr);
        }
        public static DataTable GetDataMathangSanxuat(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataMathangSanxuat", "DATA", myArr);
        }

        public static DataTable GetData_Sx_Dondat_TungayDenngay(string Madonvi, int Trangthai, DateTime Tungay, DateTime Denngay, string Maptnx)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Maptnx", SqlDbType.VarChar), Maptnx);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_Dondat_TungayDenngay", "DATA", myArr);
        }
        public static DataTable GetData_Sx_Lenhsx_TungayDenngay(string Madonvi, int Trangthai, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_Lenhsx_TungayDenngay", "DATA", myArr);
        }
        public static DataTable GetData_Sx_LenhsxThanhPham_TungayDenngay(string Madonvi, int Trangthai, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_LenhsxThanhPham_TungayDenngay", "DATA", myArr);
        }
        public static DataTable GetData_Sx_LenhsxNVL_TungayDenngay(string Madonvi, int Trangthai, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_LenhsxNVL_TungayDenngay", "DATA", myArr);
        }
        public static DataTable GetData_Sx_Dutinhnvl_TungayDenngay(string Madonvi, int Trangthai, DateTime Tungay, DateTime Denngay, int Loaigiaodich)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Loaigiaodich", SqlDbType.Int), Loaigiaodich);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_Dutinhnvl_TungayDenngay", "DATA", myArr);
        }
        public static DataTable PRO_Sx_NvXacnhan_GetData(string Madonvi, string Maxacnhan, string Manghiepvu)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manghiepvu", SqlDbType.VarChar), Manghiepvu);
            parameters.Add(new SqlParameter("@Maxacnhan", SqlDbType.VarChar), Maxacnhan);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("PRO_Sx_NvXacnhan_GetData", "DATA", myArr);
        }
        public static DataTable PRO_Sx_NvXacnhan_Create(string Madonvi, string Maxacnhan, string Manghiepvu)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manghiepvu", SqlDbType.VarChar), Manghiepvu);
            parameters.Add(new SqlParameter("@Maxacnhan", SqlDbType.VarChar), Maxacnhan);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("PRO_Sx_NvXacnhan_Create", "DATA", myArr);
        }
        //public static DataTable PRO_Sx_NvXacNhan_Nguoidungxacnhan(string Madonvi, string Maxacnhan, string Manghiepvu, string Manhanvien, int Trangthai, string Ghichu)
        //{
        //    ListDictionary parameters = new ListDictionary();
        //    parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
        //    parameters.Add(new SqlParameter("@Manghiepvu", SqlDbType.VarChar), Manghiepvu);
        //    parameters.Add(new SqlParameter("@Maxacnhan", SqlDbType.VarChar), Maxacnhan);
        //    parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
        //    parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
        //    parameters.Add(new SqlParameter("@Ghichu", SqlDbType.NVarChar), Ghichu);
        //    DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
        //    parameters.CopyTo(myArr, 0);
        //    return ExecuteSPReader("PRO_Sx_NvXacNhan_Nguoidungxacnhan", "DATA", myArr);
        //}

        public static int PRO_Sx_NvXacNhan_Nguoidungxacnhan(string Madonvi, string Maxacnhan, string Manghiepvu, string Manhanvien, int Trangthai, string Ghichu)
        {
            if (string.IsNullOrEmpty(Manghiepvu))
                return 5;
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manghiepvu", SqlDbType.VarChar), Manghiepvu);
            parameters.Add(new SqlParameter("@Maxacnhan", SqlDbType.VarChar), Maxacnhan);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Ghichu", SqlDbType.NVarChar), Ghichu);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return int.Parse(ExecuteSPReaderReturnIntValue("PRO_Sx_NvXacNhan_Nguoidungxacnhan", "@ReturnVal", myArr).ToString());
        }

        public static int PRO_Sx_NvXacNhan_Checkphieu(string Madonvi, string Maxacnhan, string Manghiepvu)
        {
            if (string.IsNullOrEmpty(Manghiepvu))
                return 1;
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manghiepvu", SqlDbType.VarChar), Manghiepvu);
            parameters.Add(new SqlParameter("@Maxacnhan", SqlDbType.VarChar), Maxacnhan);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return int.Parse(ExecuteSPReaderReturnIntValue("PRO_Sx_NvXacNhan_Checkphieu", "@ReturnVal", myArr).ToString());
        }

        public static DataTable PRO_Sx_NvXacNhan_Checknguoidung(string Madonvi, string Maxacnhan, string Manghiepvu, string Manhanvien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manghiepvu", SqlDbType.VarChar), Manghiepvu);
            parameters.Add(new SqlParameter("@Maxacnhan", SqlDbType.VarChar), Maxacnhan);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("PRO_Sx_NvXacNhan_Checknguoidung", "DATA", myArr);
        }

        public static DataTable GetData_Sx_SxDmXacnhanct(string Madonvi, string Maxacnhan)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maxacnhan", SqlDbType.VarChar), Maxacnhan);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_SxDmXacnhanct", "DATA", myArr);
        }
        public static DataTable GetData_Sx_SxKyGiathanhct(string Madonvi, string Maky, int Thangdky, int Namdky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            parameters.Add(new SqlParameter("@Thangdky", SqlDbType.Int), Thangdky);
            parameters.Add(new SqlParameter("@Namdky", SqlDbType.Int), Namdky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_SxKyGiathanhct", "DATA", myArr);
        }
        public static DataTable GetData_Sx_GiaodichDodangdky(string Madonvi, int Flag)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Flag", SqlDbType.Int), Flag);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_GiaodichDodangdky", "DATA", myArr);
        }
        public static DataTable GetData_Sx_SxKyGiathanh(string Madonvi, string Maky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_SxKyGiathanh", "DATA", myArr);
        }
        public static DataTable GetData_Sx_ThanhphamTrongKyGiaThanh(string Trangthai, string Madonvi, string maNhomptnx, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_ThanhphamTrongKyGiaThanh", "DATA", myArr);
        }

        public static DataTable GetData_Sx_Denghilinhlieu_TungayDenngay(string Madonvi, int Trangthai, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_Denghilinhlieu_TungayDenngay", "DATA", myArr);
        }
        public static DataTable GetData_Sx_ThanhphamDaKhaibaoDtthcp(string Madonvi, string Madoituongtaphop, string Masieuthi, int _Case)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madoituongtaphop", SqlDbType.VarChar), Madoituongtaphop);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Case", SqlDbType.Int), _Case);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_ThanhphamDaKhaibaoDtthcp", "DATA", myArr);
        }
        public static DataTable Getdata_SxPhanbochiphichung(string Madonvi, string Maky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_SxPhanbochiphichung", "DATA", myArr);
        }
        public static DataTable Getdata_SxPhanbochiphichungSaveRef(string Madonvi, string Maky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_SxPhanbochiphichungSaveRef", "DATA", myArr);
        }
        public static DataTable Getdata_SxPhanbochiphichungct(string Madonvi, string Maky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_SxPhanbochiphichungct", "DATA", myArr);
        }
        public static DataTable Getdata_SxDoituongThcp_TrungKyGiaThanh(string Madonvi, string Maky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_SxDoituongThcp_TrungKyGiaThanh", "DATA", myArr);
        }
        public static DataTable GetData_Sx_ThanhphamDodangTrongKy(int Trangthai, string Madonvi, string maNhomptnx, string Maky, int Thangdky, int Namdky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            parameters.Add(new SqlParameter("@Thangdky", SqlDbType.Int), Thangdky);
            parameters.Add(new SqlParameter("@Namdky", SqlDbType.Int), Namdky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_ThanhphamDodangTrongKy", "DATA", myArr);
        }
        public static DataTable GetData_Sx_LenhsxNVL_ByMagiaodichpk(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_LenhsxNVL_ByMagiaodichpk", "DATA", myArr);
        }

        public static DataTable GetData_Sx_BangtinhGiathanhTrongKy(int Trangthai, string Madonvi, string maNhomptnx, string Maky, int Thangdky, int Namdky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            parameters.Add(new SqlParameter("@Thangdky", SqlDbType.Int), Thangdky);
            parameters.Add(new SqlParameter("@Namdky", SqlDbType.Int), Namdky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Sx_BangtinhGiathanhTrongKy", "DATA", myArr);
        }
        public static DataTable Getdata_Sx_DutinhNVLctByMa(string Ma, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Ma);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Sx_DutinhNVLctByMa", "DATA", myArr);
        }

        public static DataTable Getdata_SxDataImportKetchuyenChiphi(string Madonvi, string Maky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_SxDataImportKetchuyenChiphi", "DATA", myArr);
        }
        public static bool PRO_Sx_UpdateGiaNhapKho(string Madonvi, decimal Giathanh, string Masieuthi, string Maky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Giathanh", SqlDbType.Decimal), Giathanh);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("PRO_Sx_UpdateGiaNhapKho", myArr);
        }
        public static bool PRO_Sx_UpdateGiaXuatKho(string Madonvi, decimal Giathanh, string Masieuthi, string Maky)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Giathanh", SqlDbType.Decimal), Giathanh);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Maky", SqlDbType.VarChar), Maky);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("PRO_Sx_UpdateGiaXuatKho", myArr);
        }
        public static bool PRO_VTHH_UpdateGiaodichct_ChiphiMuahang(string Madonvi, decimal Chiphikhac, string Masieuthi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Chiphikhac", SqlDbType.Decimal), Chiphikhac);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("PRO_VTHH_UpdateGiaodichct_ChiphiMuahang", myArr);
        }
        public static bool PRO_VTHH_UpdateGiaodichct_PhanboChietkhau(string Madonvi, decimal Tienck, decimal Tiencknt, string Masieuthi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tienck", SqlDbType.Decimal), Tienck);
            parameters.Add(new SqlParameter("@Tiencknt", SqlDbType.Decimal), Tiencknt);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("PRO_VTHH_UpdateGiaodichct_PhanboChietkhau", myArr);
        }
        public static bool PRO_Sx_UpdateTrangthaiphieu(string Madonvi, string TableName, string Magiaodichpk, string Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tablename", SqlDbType.VarChar), TableName);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("PRO_Sx_UpdateTrangthaiphieu", myArr);
        }
        public static bool PRO_Sx_Updatestatus(string Madonvi, string TableName, string ColumMapk, string Mapk, string Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tablename", SqlDbType.VarChar), TableName);
            parameters.Add(new SqlParameter("@Colummapk", SqlDbType.VarChar), ColumMapk);
            parameters.Add(new SqlParameter("@Mapk", SqlDbType.VarChar), Mapk);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.VarChar), Trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("PRO_Sx_Updatestatus", myArr);
        }
        public static bool PRO_Sx_UpdateTrangthaibom(string Madonvi, string Magiaodichpk, int Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mactsx", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("PRO_Sx_UpdateTrangthaibom", myArr);
        }

        public static DataTable GetData_DathangctByMapkAndMadonvi(string Magiaodichpk, string Madonvi, string Madongop)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), Madongop);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DathangctByMapkAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_KehoachDathangctByMapkAndMadonvi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_KehoachDathangctByMapkAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_DenghixuathangctByMapkAndMadonvi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DenghixuathangctByMapkAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_DenghixuathangImportByMapkAndMadonvi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DenghixuathangImportByMapkAndMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_KehoachImportByMapkAndMadonvi(string Magiaodichpk, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_KehoachImportByMapkAndMadonvi", "DATA", myArr);
        }
        #endregion
        public static DataTable GetData_TIENICH_Canhbaothieuctugoc(DateTime Tungay, DateTime Denngay, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_TIENICH_Canhbaothieuctugoc", "DATA", myArr);
        }
        public static DataTable GetData_CtugocTungayDenngay(DateTime Tungay, DateTime Denngay, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_CtugocTungayDenngay", "DATA", myArr);
        }
        #region Báo cáo sản xuất
        public static DataTable BC_SX_TIENDOSANXUAT(string Madonvi, string Magiaodichpk, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_SX_TIENDOSANXUAT", "DATA", myArr);
        }
        public static DataTable BC_SX_XUATKHOTHEOLENHSX(string Madonvi, string Magiaodichpk, DateTime Tungay, DateTime Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_SX_XUATKHOTHEOLENHSX", "DATA", myArr);
        }
        public static DataTable BC_SX_XUATKHOTHEODONDAT(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_SX_XUATKHOTHEODONDAT", "DATA", myArr);
        }
        #endregion
        #region Nhân sự
        public static DataTable GetData_NS_Bangchamcongct_By_Mabangchamcong(string Mabangchamcong, string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mabangchamcong", SqlDbType.VarChar), Mabangchamcong);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_NS_Bangchamcongct_By_Mabangchamcong", "DATA", myArr);
        }
        public static DataTable GetData_NS_Bangchamcongct_By_Mabophan(string Mabophan, int Trangthaisudung)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Mabophan", SqlDbType.VarChar), Mabophan);
            parameters.Add(new SqlParameter("@Trangthaisudung", SqlDbType.Int), Trangthaisudung);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_NS_Bangchamcongct_By_Mabophan", "DATA", myArr);
        }
        public static DataTable GetDataBangluongTungayDenngay(string Madonvi, DateTime Tungay, DateTime Denngay, int Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataBangluongTungayDenngay", "DATA", myArr);
        }
        public static DataTable GetDataBangChamcongTungayDenngay(string Madonvi, DateTime Tungay, DateTime Denngay, int Trangthai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetDataBangChamcongTungayDenngay", "DATA", myArr);
        }

        #endregion

        #region Tài sản

        public static DataTable GetData_Ts_LoaitaisanByLoaihang(string Madonvi, int Loaihang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Loaihang", SqlDbType.Int), Loaihang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_LoaitaisanByLoaihang", "DATA", myArr);
        }

        public static DataTable GetData_Ts_NhomtaisanByLoaihang(string Madonvi, int Loaihang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Loaihang", SqlDbType.Int), Loaihang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_NhomtaisanByLoaihang", "DATA", myArr);
        }

        public static DataTable GetData_Ts_TsTaisanByMadonvi(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_TsTaisanByMadonvi", "DATA", myArr);
        }
        public static DataTable GetData_Ts_TsGiaodich_By_Magiaodichpk(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_TsGiaodich_By_Magiaodichpk", "DATA", myArr);
        }
        public static DataTable GetData_Ts_TsGiaodichct_By_Magiaodichpk(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_TsGiaodichct_By_Magiaodichpk", "DATA", myArr);
        }
        public static DataTable GetData_Ts_GiaodichByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, string tuNgay, string denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.VarChar), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.VarChar), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_GiaodichByTungayDenngay", "DATA", myArr);
        }

        public static DataTable GetData_Ts_KhauhaoctByTaisan(string Madonvi, string tuNgay, string denNgay, string maPhongban)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.VarChar), tuNgay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.VarChar), denNgay);
            parameters.Add(new SqlParameter("@Maphongban", SqlDbType.VarChar), maPhongban);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_KhauhaoctByTaisan", "DATA", myArr);
        }

        public static DataTable GetData_Ts_KhauhaoByTungayDenngay(string Trangthai, string Madonvi, string maNhomptnx, string tuNgay, string denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maNhomptnx", SqlDbType.VarChar), maNhomptnx);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.VarChar), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.VarChar), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_KhauhaoByTungayDenngay", "DATA", myArr);
        }
        public static DataTable GetData_Ts_TsKhauhao_By_Magiaodichpk(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_TsKhauhao_By_Magiaodichpk", "DATA", myArr);
        }
        public static DataTable GetData_Ts_TsKhauhaoct_By_Magiaodichpk(string Madonvi, string Magiaodichpk)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Ts_TsKhauhaoct_By_Magiaodichpk", "DATA", myArr);
        }
        public static DataTable BC_Ts_Sotaisancodinh(string Madonvi, int Loaihang, string Tungay, string Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Loaihang", SqlDbType.Int), Loaihang);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.Date), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.Date), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_Ts_Sotaisancodinh", "DATA", myArr);
        }
        public static DataTable BC_TS_Sotheodoitaisantainoisudung(string Madonvi, int Loaihang, string Tungay, string Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Loaihang", SqlDbType.Int), Loaihang);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.Date), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.Date), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_TS_Sotheodoitaisantainoisudung", "DATA", myArr);
        }
        public static DataTable BC_TS_Thetaisancodinh(string Madonvi, int Loaihang, string Mataisan, string Denngay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Loaihang", SqlDbType.Int), Loaihang);
            parameters.Add(new SqlParameter("@Mataisan", SqlDbType.Date), Mataisan);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.Date), Denngay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_TS_Thetaisancodinh", "DATA", myArr);
        }

        #endregion

        #region Xây dựng

        public static DataTable GetData_XD_DutoanctByMadutoan(string Madonvi, string Madutoan)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madutoan", SqlDbType.VarChar), Madutoan);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_XD_DutoanctByMadutoan", "DATA", myArr);
        }

        // Lấy dữ liệu theo  bảng công trình
        public static DataTable GetData_XD_Dulieucongtrinh(string Madonvi, string Macongtrinh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madutoan", SqlDbType.VarChar), Macongtrinh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_XD_Dulieucongtrinh", "DATA", myArr);
        }

        public static DataTable GetData_XD_DutoanTungayDenngay(string Madonvi, DateTime Tungay, DateTime Denngay, int Trangthai, int Loai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Loai", SqlDbType.Int), Loai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_XD_DutoanTungayDenngay", "DATA", myArr);
        }

        public static DataTable GetData_XD_Tonghopvattudutoan(string Madonvi, string Madutoan)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madutoan", SqlDbType.VarChar), Madutoan);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_XD_Tonghopvattudutoan", "DATA", myArr);
        }
        public static DataTable GetData_XD_Tonghopvattudutoantheocongtrinh(string Madonvi, string Madutoan)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madutoan", SqlDbType.VarChar), Madutoan);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_XD_Tonghopvattudutoantheocongtrinh", "DATA", myArr);
        }

        public static DataTable GetData_XD_ThicongctByMaOrCongtrinh(string Madonvi, string Mathicong, string Macongtrinh)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Mathicong", SqlDbType.VarChar), Mathicong);
            parameters.Add(new SqlParameter("@Macongtrinh", SqlDbType.VarChar), Macongtrinh);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_XD_ThicongctByMaOrCongtrinh", "DATA", myArr);
        }

        public static DataTable GetData_XD_Tonghopvattuthicongtheocongtrinh(string Madonvi, string Mathicong)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madutoan", SqlDbType.VarChar), Mathicong);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_XD_Tonghopvattuthicongtheocongtrinh", "DATA", myArr);
        }



        public static DataTable GetData_Lichdathangtheothu(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Lichdathangtheothu", "DATA", myArr);
        }
        #endregion

        #region Ban le moi
        public static DataTable WEB_GETNGANHNHOM(string Madonvi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("WEB_GETNGANHNHOM", "DATA", myArr);
        }
        #endregion

        #region getdata_hethong
        #endregion

        public static DataTable getdata_Dskhachhanglichdathangtheothu(string Madonvi, string maloaikhach, string maloaikhach1)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Maloaikhach", SqlDbType.VarChar), maloaikhach);
            parameters.Add(new SqlParameter("@Maloaikhach1", SqlDbType.VarChar), maloaikhach1);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("getdata_Dskhachhanglichdathangtheothu", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_PhieuExport_Dathang(string Magiaodichpk, string Madonvi, string strMadongo)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), strMadongo);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_PhieuExport_Dathang", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_Export_KhuyenMaiIntem(string strMaCT, string Madonvi, int intLoai)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@strMaCT", SqlDbType.VarChar), strMaCT);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@intLoai", SqlDbType.Int), intLoai);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_Export_KhuyenMaiIntem", "DATA", myArr);
        }
        public static DataTable BC_HANGKHONGPSGIAODICH(DateTime Tungay, DateTime Denngay, string DB_NAME_XNT, string TABLEXNT_NAME, string TABLEXNT_NAME2, string Makhohang, string Manganhhang, string Manhomhang
         , string Masieuthi, string Madonvi, string Makhachhang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME2", SqlDbType.VarChar), TABLEXNT_NAME2);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Makhachhang);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_HANGKHONGPSGIAODICH", "DATA", myArr);
        }
        public static DataTable Getdata_Thongtinmathang(string Masieuthi, string Madonvi, string DB_NAME_XNT, string TABLEXNT_NAME)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Thongtinmathang", "DATA", myArr);
        }
        public static DataTable Getdata_Mathang(string Trangthaikd, string Madonvi, string DB_NAME_XNT, string TABLEXNT_NAME, int iStatus)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@TABLEXNT_NAME", SqlDbType.VarChar), TABLEXNT_NAME);
            parameters.Add(new SqlParameter("@DB_NAME_XNT", SqlDbType.VarChar), DB_NAME_XNT);
            parameters.Add(new SqlParameter("@Trangthaikd", SqlDbType.VarChar), Trangthaikd);
            parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int), iStatus);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Mathang", "DATA", myArr);
        }
        public static DataTable Fill_GiaodichkiemkeByTungayDenngay(string Trangthai, string Madonvi, DateTime tuNgay, DateTime denNgay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@trangThai", SqlDbType.VarChar), Trangthai);
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@tuNgay", SqlDbType.DateTime), tuNgay);
            parameters.Add(new SqlParameter("@denNgay", SqlDbType.DateTime), denNgay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Fill_GiaodichkiemkeByTungayDenngay", "DATA", myArr);
        }
        public static DataTable Getdata_VTHH_KehoachDathangTheoKho(string strMakehoachGop, string Madonvi, string strMasieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@maDonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Madongop", SqlDbType.VarChar), strMakehoachGop);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), strMasieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_VTHH_KehoachDathangTheoKho", "DATA", myArr);
        }
        public static DataTable BC_VTHH_XUATHANGKIEMKECT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_XUATHANGKIEMKECT", "DATA", myArr);
        }
        public static DataTable BC_VTHH_XUATHANGKIEMKETH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_VTHH_XUATHANGKIEMKETH", "DATA", myArr);
        }
        public static DataTable BC_NHAPHANGKIEMKECT(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGKIEMKECT", "DATA", myArr);
        }
        public static DataTable BC_NHAPHANGKIEMKETH(DateTime Tungay, DateTime Denngay, string Madonvi, string MaPTNX, string Makho, string Manhacungcap, string Manganhhang, string Manhomhang, string Manguoinhap, string Masieuthi, int Trangthai, string Magiaodichpk, int Groupid)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            parameters.Add(new SqlParameter("@Denngay", SqlDbType.DateTime), Denngay);
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@MaPTNX", SqlDbType.VarChar), MaPTNX);
            parameters.Add(new SqlParameter("@Makho", SqlDbType.VarChar), Makho);
            parameters.Add(new SqlParameter("@Manhacungcap", SqlDbType.VarChar), Manhacungcap);
            parameters.Add(new SqlParameter("@Manganhhang", SqlDbType.VarChar), Manganhhang);
            parameters.Add(new SqlParameter("@Manhomhang", SqlDbType.VarChar), Manhomhang);
            parameters.Add(new SqlParameter("@Manguoinhap", SqlDbType.VarChar), Manguoinhap);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            parameters.Add(new SqlParameter("@Trangthai", SqlDbType.Int), Trangthai);
            parameters.Add(new SqlParameter("@Magiaodichpk", SqlDbType.VarChar), Magiaodichpk);
            parameters.Add(new SqlParameter("@Groupid", SqlDbType.Int), Groupid);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("BC_NHAPHANGKIEMKETH", "DATA", myArr);
        }
        public static DataTable GetData_Lichsusuagiaban(string Madonvi, string Masieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), Masieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_Lichsusuagiaban", "DATA", myArr);
        }


        #region GetData Danh Muc
        public static DataTable GetData_DM_Mathang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Mathang", "DATA", myArr);
        }
        public static DataTable GetData_DM_Nganhhang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Nganhhang", "DATA", myArr);
        }
        public static DataTable GetData_DM_Nhomhang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Nhomhang", "DATA", myArr);
        }
        public static DataTable GetData_DM_Donvitinh(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Donvitinh", "DATA", myArr);
        }
        public static DataTable GetData_DM_Vat(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Vat", "DATA", myArr);
        }
        public static DataTable GetData_DM_Khachhang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Khachhang", "DATA", myArr);
        }
        public static DataTable GetData_DM_Nhanvien(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Nhanvien", "DATA", myArr);
        }
        public static DataTable GetData_DM_Nhomkhachhang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Nhomkhachhang", "DATA", myArr);
        }
        public static DataTable GetData_DM_Khohang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Khohang", "DATA", myArr);
        }
        public static DataTable GetData_DM_BarcodeByMasieuthi(string maDonVi, string maSieuthi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            parameters.Add(new SqlParameter("@Masieuthi", SqlDbType.VarChar), maSieuthi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_BarcodeByMasieuthi", "DATA", myArr);
        }
        public static DataTable GetData_DM_Dmptnx(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Dmptnx", "DATA", myArr);
        }
        public static DataTable Getdata_DM_DinhmucKmNganhhang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_DM_DinhmucKmNganhhang", "DATA", myArr);
        }

        public static DataTable Getdata_KT_ThutientheoxebyMadongop(string maDonVi, string maGiaodichdongop)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            parameters.Add(new SqlParameter("@Magiaodichdongop", SqlDbType.VarChar), maGiaodichdongop);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_ThutientheoxebyMadongop", "DATA", myArr);
        }

        public static DataTable Getdata_Giaodichbymucdouutien(string maDonVi, string mucdouutien)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            parameters.Add(new SqlParameter("@Mucdouutien", SqlDbType.VarChar), mucdouutien);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_Giaodichbymucdouutien", "DATA", myArr);
        }

        public static DataTable GetData_DM_Bohang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Bohang", "DATA", myArr);
        }

        public static DataTable GetData_DM_Bohangct(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Bohangct", "DATA", myArr);
        }

        public static DataTable GetData_DM_Bacode(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Bacode", "DATA", myArr);
        }

        public static DataTable GetData_DM_Khuyenmai(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Khuyenmai", "DATA", myArr);
        }

        public static DataTable GetData_DM_Khuyenmaict(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Khuyenmaict", "DATA", myArr);
        }

        public static DataTable GetData_DM_Nganhang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Nganhang", "DATA", myArr);
        }

        public static DataTable GetData_DM_Chinhanhnganhang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Chinhanhnganhang", "DATA", myArr);
        }

        public static DataTable GetData_DM_Taikhoannganhang(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Taikhoannganhang", "DATA", myArr);
        }

        public static DataTable GetData_DM_Tuyenduong(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Tuyenduong", "DATA", myArr);
        }

        public static DataTable GetData_DM_Nhomvuviec(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Nhomvuviec", "DATA", myArr);
        }

        public static DataTable GetData_DM_Vuviec(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Vuviec", "DATA", myArr);
        }

        public static DataTable GetData_DM_Nhomkmcp(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Nhomkmcp", "DATA", myArr);
        }

        public static DataTable GetData_DM_Dmkmcp(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_DM_Dmkmcp", "DATA", myArr);
        }
        #endregion

        public static DataTable GetData_GD_Giaodich(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Giaodich", "DATA", myArr);
        }

        public static DataTable GetData_GD_Giaodichct(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Giaodichct", "DATA", myArr);
        }

        public static DataTable GetData_GD_Giaodichcttk(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Giaodichcttk", "DATA", myArr);
        }

        public static DataTable GetData_GD_Giaodichctkm(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Giaodichctkm", "DATA", myArr);
        }

        public static DataTable GetData_GD_Giaodichdongop(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Giaodichdongop", "DATA", myArr);
        }

        public static DataTable GetData_GD_Quy(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Quy", "DATA", myArr);
        }

        public static DataTable GetData_GD_Quyct(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Quyct", "DATA", myArr);
        }

        public static DataTable GetData_GD_Giaodichlinkquy(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Giaodichlinkquy", "DATA", myArr);
        }
        public static DataTable Getdata_KT_MaxNamChotSo(string maDonVi)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), maDonVi);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("Getdata_KT_MaxNamChotSo", "DATA", myArr);
        }

        public static DataTable GetData_GD_Sodudauky(int thang, int nam)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Thang", SqlDbType.Int), thang);
            parameters.Add(new SqlParameter("@Nam", SqlDbType.Int), nam);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Sodudauky", "DATA", myArr);
        }

        public static DataTable GetData_GD_Thutientheoxe(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Thutientheoxe", "DATA", myArr);
        }

        public static DataTable GetData_GD_Thutientheoxect(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Thutientheoxect", "DATA", myArr);
        }
        public static bool Insert_Nhanvienkho(string Madonvi, string Manhanvien, string Makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("Insert_Nhanvienkho", myArr);
        }
        public static bool Insert_Nhanvientuyen(string Madonvi, string Manhanvien, string Matuyen)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Matuyen", SqlDbType.VarChar), Matuyen);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("Insert_Nhanvientuyen", myArr);
        }
        public static bool Delete_Nhanvienkho(string Madonvi, string Manhanvien, string Makhohang)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Makhohang", SqlDbType.VarChar), Makhohang);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("Delete_Nhanvienkho", myArr);
        }
        public static bool Delete_Nhanvientuyen(string Madonvi, string Manhanvien, string Matuyen)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Madonvi", SqlDbType.VarChar), Madonvi);
            parameters.Add(new SqlParameter("@Manhanvien", SqlDbType.VarChar), Manhanvien);
            parameters.Add(new SqlParameter("@Matuyen", SqlDbType.VarChar), Matuyen);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReaderReturnBool("Delete_Nhanvientuyen", myArr);
        }
        public static DataTable GetData_GD_Ctugoc(DateTime Tungay)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@Tungay", SqlDbType.DateTime), Tungay);
            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);
            return ExecuteSPReader("GetData_GD_Ctugoc", "DATA", myArr);
        }
    }
}
