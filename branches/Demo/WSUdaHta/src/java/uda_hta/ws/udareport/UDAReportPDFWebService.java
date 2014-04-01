/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package uda_hta.ws.udareport;

import java.io.IOException;
import java.io.InputStream;
import java.util.List;
import java.sql.*;
import java.util.ArrayList;
import java.util.Properties;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.jws.WebService;
import javax.jws.WebMethod;
import javax.jws.WebParam;
import uda_hta.data.PathDateData;

/**
 *
 * @author vaio
 */
@WebService(serviceName = "UDAReportPDFWebService")
public class UDAReportPDFWebService {

    /**
     * Web service operation
     */
    @WebMethod(operationName = "PatientReportDocumentByRgstrNum")
    public List<PathDateData> PatientReportDocumentByRgstrNum(@WebParam(name = "PatientRegstrNum") String PatientRegstrNum) {
        
        Connection conn = null;
        long patientUdaId = 0;
        List<PathDateData> resultList = new ArrayList();
        Properties prop = new Properties();

        InputStream is = Thread.currentThread().getContextClassLoader()
    			.getResourceAsStream("config.properties");
        try {
            prop.load(is);
        } catch (IOException ex) {
            Logger.getLogger(UDAReportPDFWebService.class.getName()).log(Level.SEVERE, null, ex);
        }

        try
        {
            // - conectarme a la base patient_info_db
            String url = prop.getProperty("patient_db.url");
            Class.forName ("com.mysql.jdbc.Driver");
            conn = DriverManager.getConnection (url,prop.getProperty("db.usr"),prop.getProperty("db.pswd"));

            // - obtener el udaId del paciente con numero de registro PatientRegstrNum
            Statement stmt = conn.createStatement();
            String query = "SELECT idPatient FROM patient_info_db.patient WHERE register_number = '" + PatientRegstrNum + "';";
            ResultSet rs = stmt.executeQuery(query);
            if(rs.next())
            {
                patientUdaId = rs.getInt(1);
            }
            else
            {                
                //no existe el paciente en la base, no seguir procesando
                return null;
            }
            
            //cierro la conexion con la base de pacientes
            conn.close();        
            
            // - conectarme a la base uda_hta_db
            url = prop.getProperty("uda_db.url");
            Class.forName ("com.mysql.jdbc.Driver");
            conn = DriverManager.getConnection (url,prop.getProperty("db.usr"),prop.getProperty("db.pswd"));

            // - obtener paths de los reportes asociados a ese paciente
            stmt = conn.createStatement();
            String columns = "begin_date,report_path";
            String conditions = "patientuda_idPatientUda = " + patientUdaId;
            query = "SELECT " + columns + " FROM udahta_db.report WHERE " + conditions + ";";
            rs = stmt.executeQuery(query);
            
            //recorro el resultado de la consulta y agrego a lista
            while(rs.next())
            {
                PathDateData data = new PathDateData(rs.getString(2),rs.getDate(1));
                resultList.add(data);
            }                      

            conn.close();
            
            // - devolver paths        
            return resultList;
        }
        catch (Exception e)
        {
            Logger.getLogger(UDAReportPDFWebService.class.getName()).log(Level.SEVERE, null, e);
            return null;
        }

    }

    /**
     * Web service operation
     */
    @WebMethod(operationName = "PatientReportDocDateFilter")
    public List<PathDateData> PatientReportDocDateFilter(@WebParam(name = "PatientRegstrNum") String PatientRegstrNum, @WebParam(name = "DateIni") java.util.Date DateIni, @WebParam(name = "DateEnd") java.util.Date DateEnd) {

        Connection conn = null;
        long patientUdaId = 0;
        List<PathDateData> resultList = new ArrayList();

        Properties prop = new Properties();
        InputStream is = Thread.currentThread().getContextClassLoader()
    			.getResourceAsStream("config.properties");
        try {
            prop.load(is);
        } catch (IOException ex) {
            Logger.getLogger(UDAReportPDFWebService.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        try
        {
            // - conectarme a la base patient_info_db
            String url = prop.getProperty("patient_db.url");
            Class.forName ("com.mysql.jdbc.Driver");
            conn = DriverManager.getConnection (url,prop.getProperty("db.usr"),prop.getProperty("db.pswd"));

            // - obtener el udaId del paciente con numero de registro PatientRegstrNum
            Statement stmt = conn.createStatement();
            String query = "SELECT idPatient FROM patient_info_db.patient WHERE register_number = '" + PatientRegstrNum + "';";
            ResultSet rs = stmt.executeQuery(query);
            if(rs.next())
            {
                patientUdaId = rs.getInt(1);
            }
            else
            {                
                //no existe el paciente en la base, no seguir procesando
                return null;
            }
            
            //cierro la conexion con la base de pacientes
            conn.close();        
            
            // - conectarme a la base uda_hta_db
            url = prop.getProperty("uda_db.url");
            Class.forName ("com.mysql.jdbc.Driver");
            conn = DriverManager.getConnection (url,prop.getProperty("db.usr"),prop.getProperty("db.pswd"));

            // - obtener paths de los reportes asociados a ese paciente
            stmt = conn.createStatement();
            String columns = "begin_date,report_path";
            String conditions = String.format("patientuda_idPatientUda = %s AND "
                    + "begin_date >= '%s' AND begin_date <= '%s'",patientUdaId,DateIni,DateEnd);
            query = String.format("SELECT %s FROM udahta_db.report WHERE %s;",columns,conditions);
            rs = stmt.executeQuery(query);
            
            //recorro el resultado de la consulta y agrego a lista
            while(rs.next())
            {
                PathDateData data = new PathDateData(rs.getString(2),rs.getDate(1));
                resultList.add(data);
            }                      

            conn.close();
            
            // - devolver paths        
            return resultList;
        }
        catch (Exception e)
        {
            return null;
        }        
        
    }
}
