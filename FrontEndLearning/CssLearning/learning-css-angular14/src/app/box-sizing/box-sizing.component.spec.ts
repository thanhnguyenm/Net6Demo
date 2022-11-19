import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoxSizingComponent } from './box-sizing.component';

describe('BoxSizingComponent', () => {
  let component: BoxSizingComponent;
  let fixture: ComponentFixture<BoxSizingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BoxSizingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BoxSizingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
