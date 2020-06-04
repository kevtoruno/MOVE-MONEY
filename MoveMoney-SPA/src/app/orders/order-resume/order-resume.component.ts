import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { OrderToProcess } from 'app/_models/OrderToProcess';
import { OrderService } from 'app/_services/order.service';
import { AlertifyService } from 'app/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order-resume',
  templateUrl: './order-resume.component.html',
  styleUrls: ['./order-resume.component.css']
})
export class OrderResumeComponent implements OnInit {
  @Output() isProcessed = new EventEmitter<boolean>();
  @Input() order: any;

  constructor(private orderService: OrderService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    console.log(this.order);
  }

  processCanceled() {
    this.isProcessed.emit(false);
  }

  processOrder() {
     this.orderService.processOrder(this.order.userId, this.order.id).subscribe(next => {
      this.alertify.success('Order has been fully processed.');
      this.router.navigate(['orders/' + this.order.id]);
     }, error => {
       console.log(error);
       this.alertify.error(error);
     })
  }
}
