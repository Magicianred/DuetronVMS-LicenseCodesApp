﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace VMS.DatabaseLogicLayer
{
    public class DLL
    {
        SqlConnection con; //Ana görevi C# ile sql arasındaki bağlantıyı sağlamak
        SqlCommand cmd; //sorguları sql server a göndermemizi ve calistirmemizi sağlıyor
        SqlDataReader reader; //sql tarafından çekilen datayı c# tarafında karşıladığımız bir nesnemiz
        int ReturnValues; //etkilnen kayıt sayılarını(insert,update vs.) sql tarafından c#tarafına aktarılınca bunu tutmayı sağlar

        public DLL()
        {
            con = new SqlConnection(
                @"Password=Ahmetfaruk1!;Persist Security Info=True;User ID=sa;Initial Catalog=LicenseCodes;Data Source=LAPTOP-A1R7KQJ1\DENEME"); // sqlcon nesnemizi örnekledik
                                                                                                                                                 // lokalde çalışınca ".", eğer uzak bağlantı ise ip adresi girilir oraya

        }
    
        public void BaglantiAyarla()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {//connecitonun durumuna göre davranış belirleyeceğiz.
                con.Open();
            }
            else
                con.Close();
        }

        public int SistemKontrolu(Entities.UsersTable U)
        {
            try
            {
                cmd = new SqlCommand("select count(*) from UsersTable where KullaniciAdi =@KullaniciAdi and Sifre=@Sifre", con);
                //count dediğim için tek bir hücre dönecek
                cmd.Parameters.Add("@KullaniciAdi", SqlDbType.NVarChar).Value = U.KullaniciAdi;
                cmd.Parameters.Add("@Sifre", SqlDbType.NVarChar).Value = U.Sifre;
                BaglantiAyarla();
                ReturnValues = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {

                string deneme = ex.Message;
            }
            finally
            {
                BaglantiAyarla();
            }
            return ReturnValues;
        }

        public int MusteriEkle(Entities.ClientTable C)
        {
            try
            {
                cmd = new SqlCommand("insert into ClientTable (UC,ClientName) values (@UC,@ClientName)", con);
                cmd.Parameters.Add("@UC", SqlDbType.Int).Value = C.UC;
                cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = C.ClientName;
                BaglantiAyarla(); //sql ile c#arasındaki baglantıyı açar
                ReturnValues= cmd.ExecuteNonQuery();//hazırlamış olduğum insert sorgusunu sql e gönderir

            }
            catch(Exception ex) {
                
            }
            finally
            {
                BaglantiAyarla();
            }
            return ReturnValues;
        
        }
     
        public int MusteriDüzenle(Entities.ClientTable C)
        {
            try
            {
                cmd = new SqlCommand("Update ClientTable set ClientName=@ClientName where UC=@UC ", con);
                cmd.Parameters.Add("@UC", SqlDbType.Int).Value = C.UC;
                cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = C.ClientName;
                BaglantiAyarla();
                ReturnValues = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                BaglantiAyarla();
            }
            return ReturnValues;
        }

        public int MusteriSil(Entities.ClientTable C, out string hata)
        {
            hata = "";
            try
            {
                cmd = new SqlCommand("delete ClientTable where UC=@UC", con);
                cmd.Parameters.Add("@UC", SqlDbType.Int).Value = C.UC;
                BaglantiAyarla();
                ReturnValues = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                hata = ex.Message;
            }
            finally
            {
                BaglantiAyarla();
            }
            return ReturnValues;
        }

        public int SatisKaydiEkle(Entities.SalesTable S,out string errormessage)
        {
            errormessage = "";
            try
            {
                cmd = new SqlCommand("insert into SalesTable (ULC,UC,ClientName,Camera,NVR,VideoDuvar,IsIstasyonu,Keyboard,SatisTarihi,LisansKodu,DonanimID,Notes) values (@ULC,@UC,@ClientName,@Camera,@NVR,@VideoDuvar,@IsIstasyonu,@Keyboard,@SatisTarihi,@LisansKodu,@DonanimID,@Notes)", con);
                cmd.Parameters.Add("@ULC", SqlDbType.Int).Value = S.ULC;
                cmd.Parameters.Add("@UC", SqlDbType.Int).Value = S.UC;
                cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = S.ClientName;
                cmd.Parameters.Add("@Camera", SqlDbType.Int).Value = S.Camera;
                cmd.Parameters.Add("@NVR", SqlDbType.Int).Value = S.NVR;
                cmd.Parameters.Add("@VideoDuvar", SqlDbType.Int).Value = S.VideoDuvar;
                cmd.Parameters.Add("@IsIstasyonu", SqlDbType.Int).Value = S.IsIstasyonu;
                cmd.Parameters.Add("@Keyboard", SqlDbType.Int).Value = S.Keyboard;
                cmd.Parameters.Add("@SatisTarihi", SqlDbType.Date).Value = S.SatisTarihi;
                cmd.Parameters.Add("@LisansKodu", SqlDbType.VarChar).Value = S.LisansKodu;
                cmd.Parameters.Add("@DonanimID", SqlDbType.VarChar).Value = S.DonanimID;
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar).Value = S.Notes;
                BaglantiAyarla();
                ReturnValues = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errormessage= ex.Message;

            }
            finally
            {
                BaglantiAyarla();
            }
            return ReturnValues;
        }

        public int SatisKaydiDuzenle(Entities.SalesTable S)
        {
            try
            {
                cmd = new SqlCommand("update SalesTable set Camera=@Camera, NVR=@NVR, IsIstasyonu=@IsIstasyonu, Keyboard=@Keyboard, SatisTarihi=@SatisTarihi, LisansKodu=@LisansKodu, DonanimID=@DonanimID, Notes=@Notes where ULC=@ULC", con);
                cmd.Parameters.Add("@ULC", SqlDbType.Int).Value = S.ULC;
                //cmd.Parameters.Add("@UC", SqlDbType.Int).Value = S.UC;
                //cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = S.ClientName;
                cmd.Parameters.Add("@Camera", SqlDbType.Int).Value = S.Camera;
                cmd.Parameters.Add("@NVR", SqlDbType.Int).Value = S.NVR;
                cmd.Parameters.Add("@VideoDuvar", SqlDbType.Int).Value = S.VideoDuvar;
                cmd.Parameters.Add("@IsIstasyonu", SqlDbType.Int).Value = S.IsIstasyonu;
                cmd.Parameters.Add("@Keyboard", SqlDbType.Int).Value = S.Keyboard;
                cmd.Parameters.Add("@SatisTarihi", SqlDbType.Date).Value = S.SatisTarihi;
                cmd.Parameters.Add("@LisansKodu", SqlDbType.VarChar).Value = S.LisansKodu;
                cmd.Parameters.Add("@DonanimID", SqlDbType.VarChar).Value = S.DonanimID;
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar).Value = S.Notes;
                BaglantiAyarla();
                ReturnValues = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                BaglantiAyarla();
            }
            return ReturnValues;
        }

        public int SatisKaydiSil(Entities.SalesTable S)
        {
            
            try
            {
                cmd = new SqlCommand("delete SalesTable where ULC=@ULC", con);
                cmd.Parameters.Add("@ULC", SqlDbType.Int).Value = S.ULC;
                BaglantiAyarla();
                ReturnValues = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                BaglantiAyarla();
            }
            return ReturnValues;
        }
    }
}
