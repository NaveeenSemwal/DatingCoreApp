import { Component, inject, input, Input, OnInit, output, Signal } from '@angular/core';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { take } from 'rxjs';
import { MemberCardComponent } from '../member-card/member-card.component';
import { Member } from '../../_models/member';
import { DecimalPipe, NgClass, NgFor, NgIf, NgStyle } from '@angular/common';
import { AccountService } from '../../_services/account.service';
import { environment } from '../../../environments/environment';
import { MembersService } from '../../_services/members.service';
import { Photo } from '../../_models/photo';


@Component({
  selector: 'app-photo-editor',
  standalone : true,
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css'],
  imports: [NgIf, NgFor, NgStyle, NgClass, FileUploadModule, DecimalPipe],
})
export class PhotoEditorComponent implements OnInit {

  member =  input.required<Member>();
  memberChange = output<Member>();
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  hasAnotherDropZoneOver= false;
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);
  baseUrl = environment.apiUrl;
 

  ngOnInit(): void {
    this.initializeUploader();  
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  public fileOverAnother(e:any):void {
    this.hasAnotherDropZoneOver = e;
  }

  setMainPhoto(photo: Photo) {
    this.memberService.setMainPhoto(photo.id).subscribe({
      next: _ => {
        const user = this.accountService.currentUser();
        if (user) {

          // This is for updating the account service by updating the setCurrentUser method
          user.photoUrl = photo.url; // This will update the main profile pic
          this.accountService.setCurrentUser(user)
        }

        // This is for updating the member using the Output property
        // https://www.w3schools.com/howto/howto_js_spread_operator.asp
       const updatedMember = {...this.member()}
        updatedMember.photoUrl = photo.url;
        updatedMember.photos.forEach(p => {
          if (p.isMain) p.isMain = false;
          if (p.id === photo.id) p.isMain = true;
        });
        this.memberChange.emit(updatedMember)
      }
    })
  }

  initializeUploader() {

    // This will directly upload the photos and add the new photo to the current member photos
    this.uploader = new FileUploader({

      url: this.baseUrl + "v1/users/add-photo",
      authToken: 'Bearer ' + this.accountService.currentUser()?.token,  // Its not using Angular HTTP Request, so intercepter won't get used. So explicitly need to send token
      isHTML5: true,
      allowedFileType: ['image'], // Can accept any type of image
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,  // This is the max size cloudanary allows

    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item,response,status,Headers) => {

      if (response) {

        const photo = JSON.parse(response);
        let mem  = this.member();
         mem.photos.push(photo);
         this.memberChange.emit(mem);
      }
    }
  }

}
