import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PseudoElementComponent } from './pseudo-element.component';

describe('PseudoElementComponent', () => {
  let component: PseudoElementComponent;
  let fixture: ComponentFixture<PseudoElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PseudoElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PseudoElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
