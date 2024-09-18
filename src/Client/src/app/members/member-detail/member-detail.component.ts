import { ThisReceiver } from '@angular/compiler';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../_services/account.service';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
// import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
// import { Member } from 'src/app/_models/member';
// import { MembersService } from 'src/app/_services/members.service';
import { GalleryModule, GalleryItem, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
  imports : [TabsModule, GalleryModule]
})
export class MemberDetailComponent implements OnInit {

  // member: Member | undefined;

  // galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: GalleryItem[] = [];

  // constructor(private , private route: ActivatedRoute) { }

  private accountService = inject(AccountService);
  private membersService = inject(MembersService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  member: Member = {} as Member;

  ngOnInit(): void {

     this.loadMember();

    // this.galleryOptions = [

    //   {
    //     width: '500px',
    //     height: '500px',
    //     thumbnailsColumns: 4,
    //     imageAnimation: NgxGalleryAnimation.Slide,
    //     imagePercent: 100,
    //     preview: false
    //   }
    // ];

   
  }

  getImages() {

    if (!this.member) return [];

    for (const photo of this.member.photos) {

      this.galleryImages.push( new ImageItem({

      src : photo.url,
      thumb : photo.url
      }));
    }
    return this.galleryImages;
  }

  loadMember() {
    const username = this.route.snapshot.params["username"];

    if (!username) return;

    this.membersService.getMember(username).subscribe({
      next: member => {
        this.member = member;

       this.getImages();
      },
      error: (error: any) => { console.log(error); }
    })
  }

}
