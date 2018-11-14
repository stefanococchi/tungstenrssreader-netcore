import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NewslistComponent } from './newslist/newslist.component';

import { MatDialogModule } from '@angular/material/dialog';
import { NewsItemDialogComponent } from './news-item-dialog/news-item-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    NewslistComponent,
    NewsItemDialogComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [NewsItemDialogComponent]
})
export class AppModule { }
