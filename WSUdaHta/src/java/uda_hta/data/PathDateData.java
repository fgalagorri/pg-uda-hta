/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package uda_hta.data;

import java.util.Date;

/**
 *
 * @author vaio
 */
public class PathDateData {
    
    private String repPath;
    private Date repDate;    
    
    public PathDateData(){
        repPath = "";
        repDate = new Date();
    }
    
    public PathDateData(String path,Date date){
        repPath = path;
        repDate = date;
    }
    
    public String getRepPath(){
        return repPath;
    }
    
    public Date getRepDate(){
        return repDate;
    }
}
