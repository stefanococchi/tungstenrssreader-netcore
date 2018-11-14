import { Component, OnInit, Injectable } from '@angular/core';
import { NewslistService } from './../newslist.service';
import { MatDialog, MatDialogConfig } from "@angular/material";
import { NewsItemDialogComponent } from './../news-item-dialog/news-item-dialog.component';
import { NewsItem } from './../newsitem';

@Component({
  selector: 'app-newslist',
  templateUrl: './newslist.component.html',
  styleUrls: ['./newslist.component.css'],
  providers: [NewslistService]
})

export class NewslistComponent implements OnInit {
  newsList: NewsItem[];

  constructor(private dialog: MatDialog, private newsListService: NewslistService) {
   }

  ngOnInit() {
    this.getNews();
  }

  getNews() {
    this.newsListService.getNews().subscribe(
      data => { this.newsList = data },
      err => console.error(err),
      () => console.log("loaded news")
    );
  }
  readNewsItem(item) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = true;

    dialogConfig.data = item;

    this.dialog.open(NewsItemDialogComponent, dialogConfig);
  }
}
