import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BoxSizingComponent } from './box-sizing/box-sizing.component';
import { HomeComponent } from './home/home.component';
import { CssSelectorComponent } from './cssselector/cssselector.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import { PseudoClassComponent } from './pseudo-class/pseudo-class.component';
import { PseudoElementComponent } from './pseudo-element/pseudo-element.component';
import { FlexboxComponent } from './flexbox/flexbox.component';
import { GridlayoutComponent } from './gridlayout/gridlayout.component';

@NgModule({
  declarations: [
    AppComponent,
    BoxSizingComponent,
    HomeComponent,
    CssSelectorComponent,
    PseudoClassComponent,
    PseudoElementComponent,
    FlexboxComponent,
    GridlayoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
