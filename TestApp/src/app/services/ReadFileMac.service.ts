import { Injectable } from '@angular/core';
import { File } from '@ionic-native/file/ngx';
import { readFile } from 'fs';

@Injectable({
  providedIn: 'root'
})
export class ReadFileMacService {

constructor() {}


public ReadMacFile() {

  var macAdress:string;

  window.requestFileSystem(LocalFileSystem.PERSISTENT,0,function(fs){

    fs.root.getFile(".bluName.txt",{create: false,exclusive : false},function(fileEntry){

        
        fileEntry.file(function(file){
          var reader = new FileReader();

          reader.onloadend = function(){
            macAdress = this.result.toString();
          }
          reader.readAsText(file);
        });
    });
    
  });
  return macAdress;
 }



}
