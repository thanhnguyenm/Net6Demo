import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PseudoClassComponent } from './pseudo-class.component';

describe('PseudoClassComponent', () => {
  let component: PseudoClassComponent;
  let fixture: ComponentFixture<PseudoClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PseudoClassComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PseudoClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
