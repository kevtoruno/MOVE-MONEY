import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-order-resume',
  templateUrl: './order-resume.component.html',
  styleUrls: ['./order-resume.component.css']
})
export class OrderResumeComponent implements OnInit {
  @Output() isProcessed = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit() {
  }

  processCanceled() {
    this.isProcessed.emit(false);
  }
}
