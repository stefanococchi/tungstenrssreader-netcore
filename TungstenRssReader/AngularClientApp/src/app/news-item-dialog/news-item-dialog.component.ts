import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { NewsItem, NewsThumbnail } from "../newsitem";

@Component({
  selector: 'app-news-item-dialog',
  templateUrl: './news-item-dialog.component.html',
  styleUrls: ['./news-item-dialog.component.css']
})
export class NewsItemDialogComponent implements OnInit {
  title: string;
  description: string;
  link: string;
  guid: string;
  pubDate: string;
  thumbnail: NewsThumbnail;

  constructor(
    private dialogRef: MatDialogRef<NewsItemDialogComponent>,
    @Inject(MAT_DIALOG_DATA) { title, description, link, guid, pubDate, thumbnail }: NewsItem) {
      this.title = title;
      this.description = description;
      this.link = link;
      this.guid = guid;
      this.pubDate = pubDate;
      this.thumbnail = thumbnail;
  }

  ngOnInit() {
  }
  close() {
    this.dialogRef.close();
  }
}
