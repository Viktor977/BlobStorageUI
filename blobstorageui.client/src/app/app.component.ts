import { Component, OnInit } from '@angular/core';
import { BlobService } from 'src/services/blob.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  email:string='';
  constructor(private service:BlobService) {}

  ngOnInit() {} 
  
  loadFile(event: any) {
    let confirmed = window.confirm("Thank You for load new file.Check your email!");
    console.log(event);
    const file: File = event.target.files[0];
    console.log(file);
    if (file) {
      this.service.uploadFile(file).subscribe(
        response => {
          console.log('File uploaded successfully:', response);
        },
        error => {
          console.error('Error uploading file:', error);
        }
      );
    }
  }
 
}
