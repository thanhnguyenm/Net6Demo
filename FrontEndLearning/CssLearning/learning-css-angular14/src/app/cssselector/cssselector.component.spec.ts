import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CssselectorComponent } from './cssselector.component';

describe('CssselectorComponent', () => {
  let component: CssselectorComponent;
  let fixture: ComponentFixture<CssselectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CssselectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CssselectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
