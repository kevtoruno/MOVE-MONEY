import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { OrderToProcess } from 'app/_models/OrderToProcess';

@Component({
  selector: 'app-order-resume',
  templateUrl: './order-resume.component.html',
  styleUrls: ['./order-resume.component.css']
})
export class OrderResumeComponent implements OnInit {
  @Output() isProcessed = new EventEmitter<boolean>();
  @Input() order: any;

  constructor() { }

  ngOnInit() {
    console.log(this.order);
  }

  processCanceled() {
    this.isProcessed.emit(false);
  }
}
